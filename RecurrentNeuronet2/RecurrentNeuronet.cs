using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurrentNeuronet2
{
	class RecurrentNeuronet
	{
		// Параметры

		private int n; // количество "повторов" (максимальное время)
		private int r; // число нейронов на скрытом слое
		private int s; // размерность входа
		private int m; // размерность выхода
		private double[/*n+1*/][/*s*/] x; // x[t] - входной вектор номер t,
		private double[/*n+1*/][/*r*/] h; // h[t] - состояние скрытого слоя для входа x[t] (h[0]=0 ), 
		private double[/*m*/] y; // y - выход сети для входа x, 
		private double[/*r*/][/*s*/] V; // V - весовая матрица распределительного слоя,
		private double[/*r*/][/*r*/] U; // U - весовая(квадратная) матрица обратных связей скрытого слоя, 
		private double[/*r*/] a; // bh - вектор сдвигов скрытого слоя,
		private double[/*m*/][/*r*/] W; // W - весовая матрица выходного слоя,
		private double[/*m*/] b; // by - вектор сдвигов выходного слоя

		private double[/*m*/] d; // d - вектор правильных ответов

		private double[/*n+1*/][/*r*/] state;
		private double[/*m*/] finalState;

		private double[/*m*/] q;
		private double[/*n+1*/][/*r*/] p; // множители Лагранжа

		private double step;
		private double alpha_U; // скорость обучения
		private double alpha_W;
		private double alpha_V;
		private double alpha_a;
		private double alpha_b;
		private double epsilon; // точность
		private double I; // ошибка

		private bool tooSlow = false;

		public StringBuilder log;

		// Функции
		private double kf = 1;
		// f - функция активации скрытого слоя
		private double f(double state)
		{
			// 1/(1-e^(-kf*x))
			return 1 / (1 + Math.Exp(-kf * state));
		}

		// f1 = f' - производная f
		private double f1(double state)
		{
			return kf * Math.Exp(-kf * state) / Math.Pow(1 + Math.Exp(-kf * state), 2);
		}

		// g - функция активации выходного слоя
		private double g(double state)
		{
			// 1/(1-e^-x)
			return 1 / (1 + Math.Exp(-state));
		}

		// g1 = g' - производная g
		private double g1(double state)
		{
			return Math.Exp(-state) / Math.Pow(1 + Math.Exp(-state), 2);
		}


		// Прямой проход:
		private void DirectPass()
		{
			// для каждого вектора последовательности { x(1),…,x(n) } : 
			for (int t = 0; t < n; t++)
			{
				//вычисляем состояния скрытого слоя { state(1),…,state(n) } и выходы скрытого слоя { h(1),…,h(n) }
				for (int i = 0; i < r; i++)
				{
					double Suh = 0;
					for (int j = 0; j < r; j++)
						Suh += U[i][j] * h[t][j];
					double Svx = 0;
					for (int j = 0; j < s; j++)
						Svx += V[i][j] * x[t + 1][j];
					state[t + 1][i] = Suh + Svx + a[i];
					h[t + 1][i] = f(state[t + 1][i]);
				}
			}

			//вычисляем выход сети y
			for (int i = 0; i < m; i++)
			{
				double Swh = 0;
				for (int j = 0; j < r; j++)
					Swh += W[i][j] * h[n][j];
				finalState[i] = Swh + b[i];
				y[i] = g(finalState[i]);
			}
			CalculateI();
		}

		private void CalculateI()
		{
			I = 0;
			for (int i = 0; i < m; i++)
				I += (d[i] - y[i]) * (d[i] - y[i]);
		}

		// Обратный проход:
		private void BackwardPass()
		{
			// вычисляем ошибку выходного слоя q
			q = new double[m];
			for (int l = 0; l < m; l++)
				q[l] = y[l] - d[l];

			// вычисляем ошибку скрытого слоя в конечном состоянии p(n)
			p = new double[n + 1][];
			p[n] = new double[r];
			for (int l = 0; l < r; l++)
			{
				double Sqg1w = 0;
				for (int i = 0; i < m; i++)
					Sqg1w += q[i] * g1(finalState[i]) * W[i][l];
				p[n][l] = Sqg1w;
			}
			// вычисляем ошибки скрытого слоя в промежуточных состояниях p(t) (t = 1,…,n)
			for (int t = n - 1; t >= 0; t--)
			{
				p[t] = new double[r];
				for(int l = 0; l<r; l++)
				{
					double Spf1u = 0;
					for (int i = 0; i < r; i++)
						Spf1u += p[t + 1][i] * f1(state[t + 1][i]) * U[i][l];
					p[t][l] = Spf1u;
				}
			}
		}

		// 3. Вычисляем изменение весов:
		private void Change_U(bool badCase)
		{
			if (badCase)
			{
				alpha_U /= 2;
				if (alpha_U == 0)
					return;
			}
			else
			{
				double dmax = 0;
				double max = 0;
				for (int l = 0; l < r; l++)
					for (int k = 0; k < r; k++)
					{
						double tmp = 0;
						for (int t = 0; t < n; t++)
							tmp += p[t + 1][l] * f1(state[t + 1][l]) * h[t][k];
						if (dmax < tmp)
							dmax = tmp;
						if (max < U[l][k])
							max = U[l][k];
					}
				if (dmax == 0 || max == 0)
					alpha_U = step;
				else
					alpha_U = 0.1 * max / dmax;
			}

			for (int l = 0; l < r; l++)
				for (int k = 0; k < r; k++)
				{
					double Spf1h = 0;
					for (int t = 0; t < n; t++)
						Spf1h += p[t + 1][l] * f1(state[t + 1][l]) * h[t][k];
					U[l][k] -= alpha_U * Spf1h;
				}
		}

		private void Change_V(bool badCase)
		{
			if (badCase)
			{
				alpha_V /= 2;
				if (alpha_V == 0)
					return;
			}
			else
			{
				double dmax = 0;
				double max = 0;
				for (int l = 0; l < r; l++)
					for (int k = 0; k < s; k++)
					{
						double tmp = 0;
						for (int t = 0; t < n; t++)
							tmp += p[t + 1][l] * f1(state[t + 1][l]) * x[t + 1][k];
						if (dmax < tmp)
							dmax = tmp;
						if (max < V[l][k])
							max = V[l][k];
					}
				if (dmax == 0 || max == 0)
					alpha_V = step;
				else
					alpha_V = 0.1 * max / dmax;
			}

			for (int l = 0; l < r; l++)
				for (int k = 0; k < s; k++)
				{
					double Spf1x = 0;
					for (int t = 0; t < n; t++)
						Spf1x += p[t + 1][l] * f1(state[t + 1][l]) * x[t + 1][k];
					V[l][k] -= alpha_V * Spf1x;
				}
		}

		private void Change_W(bool badCase)
		{
			if (badCase)
			{
				alpha_W /= 2;
				if (alpha_W == 0)
					return;
			}
			else
			{
				double dmax = 0;
				double max = 0;
				for (int l = 0; l < W.Length; l++)
					for (int k = 0; k < W[l].Length; k++)
					{
						double tmp = q[l] * g1(finalState[l]) * h[n][k];
						if (dmax < tmp)
							dmax = tmp;
						if (max < W[l][k])
							max = W[l][k];
					}
				if (dmax == 0 || max == 0)
					alpha_W = step;
				else
					alpha_W = 0.1 * max / dmax;
			}

			for (int l = 0; l < m; l++)
				for (int k = 0; k < r; k++)
					W[l][k] -= alpha_W * q[l] * g1(finalState[l]) * h[n][k];
		}

		private void Change_a(bool badCase)
		{
			if (badCase)
			{
				alpha_a /= 2;
				if (alpha_a == 0)
					return;
			}
			else
			{
				double dmax = 0;
				double max = 0;
				for (int l = 0; l < r; l++)
				{
					double tmp = 0;
					for (int t = 0; t < n; t++)
						tmp += p[t + 1][l] * f1(state[t + 1][l]);
					if (dmax < tmp)
						dmax = tmp;
					if (max < a[l])
						max = a[l];
				}
				if (dmax == 0 || max == 0)
					alpha_a = step;
				else
					alpha_a = 0.1 * max / dmax;
			}

			for (int l = 0; l < r; l++)
			{
				double Spf1 = 0;
				for (int t = 0; t < n; t++)
					Spf1 += p[t + 1][l] * f1(state[t + 1][l]);
				a[l] -= alpha_a * Spf1;
			}
		}

		private void Change_b(bool badCase)
		{
			if (badCase)
			{
				alpha_b /= 2;
				if (alpha_b == 0)
					return;
			}
			else
			{
				double dmax = 0;
				double max = 0;
				for (int l = 0; l < m; l++)
				{
					double tmp = q[l] * g1(finalState[l]);
					if (dmax < tmp)
						dmax = tmp;
					if (max < b[l])
						max = b[l];
				}
				if (dmax == 0 || max == 0)
					alpha_b = step;
				else
					alpha_b = 0.1 * max / dmax;
			}

			for (int l = 0; l < m; l++)
				b[l] -= alpha_b * q[l] * g1(finalState[l]);
		}

		// Обратная связь

		public RecurrentNeuronet(double[/*строк*/][/*слов*/][/*размерность слова*/] enters,
		int innerLength, double accuracy, double step, int learnTime)
		{
			// сюда ещё можно исключений набросать, валидаторов
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			log = new StringBuilder("");
			
			n = MaxInnerLength(enters);
			r = innerLength;
			s = enters[0][0].Length;
			m = enters.Length;

			epsilon = accuracy;

			V = new double[r][];
			for (int i = 0; i < r; i++)
			{
				V[i] = new double[s];
				for (int j = 0; j < s; j++)
					V[i][j] = 1;
			}

			U = new double[r][];
			for (int i = 0; i < r; i++)
			{
				U[i] = new double[r];
				for (int j = 0; j < r; j++)
					U[i][j] = 1;
			}

			W = new double[m][];
			for (int i = 0; i < m; i++)
			{
				W[i] = new double[r];
				for (int j = 0; j < r; j++)
					W[i][j] = 1;
			}

			a = new double[r];
			for (int j = 0; j < r; j++)
				a[j] = 1;

			b = new double[m];
			for (int i = 0; i < m; i++)
				b[i] = 1;


			bool learnedInThisCicle;
			do
			{
				learnedInThisCicle = false;
				for (int i = 0; i < m; i++)
				{
					alpha_W = alpha_V = alpha_U = alpha_a = alpha_b = this.step = step;
					x = new double[n + 1][];
					for (int j = 0; j < n; j++)
					{
						x[j + 1] = new double[s];
						if (j < enters[i].Length)
							for (int k = 0; k < enters[i][j].Length; k++)
								x[j + 1][k] = enters[i][j][k];
					}

					y = new double[m];
					d = new double[m];
					d[i] = 1;

					h = new double[n + 1][];
					state = new double[n + 1][];
					finalState = new double[m];
					for (int j = 0; j <= n; j++)
					{
						h[j] = new double[r];
						state[j] = new double[r];
					}

					int iterations = Learn(ref learnedInThisCicle, stopwatch, learnTime);
					log.AppendFormat("string {0}: {1} iterations, I = {2}, time: {3}:{4}:{5}", i, iterations, I, 
						stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds).AppendLine();

					if (stopwatch.Elapsed.Minutes >= learnTime)
					{
						log.AppendLine("Time is off, learning stopped");
						break;
					}
					if (tooSlow)
					{
						log.AppendLine("Learning is too slow, stopped");
						break;
					}
				}
			} while (learnedInThisCicle && stopwatch.Elapsed.Minutes < learnTime && !tooSlow);
			if (!learnedInThisCicle)
				log.AppendLine("Learned successeful");
		}

		public void ContinueLearning(double[/*строк*/][/*слов*/][/*размерность слова*/] enters,
		int innerLength, double accuracy, double step, int learnTime)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			log = new StringBuilder("");
			epsilon = accuracy;

			bool learnedInThisCicle;
			do
			{
				learnedInThisCicle = false;
				for (int i = 0; i < m; i++)
				{
					alpha_W = alpha_V = alpha_U = alpha_a = alpha_b = this.step = step;
					x = new double[n + 1][];
					for (int j = 0; j < n; j++)
					{
						x[j + 1] = new double[s];
						if (j < enters[i].Length)
							for (int k = 0; k < enters[i][j].Length; k++)
								x[j + 1][k] = enters[i][j][k];
					}

					y = new double[m];
					d = new double[m];
					d[i] = 1;

					h = new double[n + 1][];
					state = new double[n + 1][];
					finalState = new double[m];
					for (int j = 0; j <= n; j++)
					{
						h[j] = new double[r];
						state[j] = new double[r];
					}

					int iterations = Learn(ref learnedInThisCicle, stopwatch, learnTime);
					log.AppendFormat("string {0}: {1} iterations, I = {2}, time: {3}:{4}:{5}", i, iterations, I,
						stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds).AppendLine();

					if (stopwatch.Elapsed.Minutes > learnTime)
					{
						log.AppendLine("Time is off, learning stopped");
						return;
					}
					if (tooSlow)
					{
						log.AppendLine("Learning is too slow, stopped");
						return;
					}
				}
			} while ((learnedInThisCicle && stopwatch.Elapsed.Minutes < learnTime && !tooSlow);
			if (!learnedInThisCicle)
				log.AppendLine("Learned successeful");
		}

		private int Learn(ref bool learnedInThisCicle, Stopwatch stopwatch, int learnTime)
		{
			DirectPass();
			int iterations = 0;
			while (I > epsilon && stopwatch.Elapsed.Minutes < learnTime)
			{
				double I_old = I;
				Learn_W();
				Learn_V();
				Learn_U();
				Learn_a();
				Learn_b();

				learnedInThisCicle = true;

				iterations++;

				if (I_old - I < 1E-15)
				{
					tooSlow = true;
					return iterations;
				}
			}
			return iterations;
		}

		private void Learn_U()
		{
			double I_old = I;
			double[][] U_old = new double[r][];
			for (int j = 0; j < r; j++)
				U_old[j] = (double[])U[j].Clone();

			BackwardPass();
			Change_U(false);
			DirectPass();

			while (I > I_old)
			{
				U = new double[r][];
				for (int j = 0; j < r; j++)
					U[j] = (double[])U_old[j].Clone();
				DirectPass();
				I_old = I;
				if (alpha_U == 0)
					break;
				BackwardPass();
				Change_U(true);
				DirectPass();
			}
		}

		private void Learn_V()
		{
			double I_old = I;
			double[][] V_old = new double[r][];
			for (int j = 0; j < r; j++)
				V_old[j] = (double[])V[j].Clone();

			BackwardPass();
			Change_V(false);
			DirectPass();

			while (I > I_old)
			{
				V = new double[r][];
				for (int j = 0; j < r; j++)
					V[j] = (double[])V_old[j].Clone();
				DirectPass();
				I_old = I;
				if (alpha_V == 0)
					break;
				BackwardPass();
				Change_V(true);
				DirectPass();
			}
		}

		private void Learn_W()
		{
			double I_old = I;
			double[][] W_old = new double[m][];
			for (int j = 0; j < m; j++)
				W_old[j] = (double[])W[j].Clone();
			
			BackwardPass();
			Change_W(false);
			DirectPass();
			
			while (I > I_old)
			{
				W = new double[m][];
				for (int j = 0; j < m; j++)
					W[j] = (double[])W_old[j].Clone();
				DirectPass();
				I_old = I;
				if (alpha_W == 0)
					break;
				BackwardPass();
				Change_W(true);
				DirectPass();
			}
		}

		private void Learn_a()
		{
			double I_old = I;
			double[] a_old = (double[])a.Clone();

			BackwardPass();
			Change_a(false);
			DirectPass();

			while (I > I_old)
			{
				double[] a = (double[])a_old.Clone();
				DirectPass();
				I_old = I;
				if (alpha_a == 0)
					break;
				BackwardPass();
				Change_a(true);
				DirectPass();
			}
		}

		private void Learn_b()
		{
			double I_old = I;
			double[] b_old = (double[])b.Clone();

			BackwardPass();
			Change_b(false);
			DirectPass();

			while (I > I_old)
			{
				double[] b = (double[])b_old.Clone();
				DirectPass();
				I_old = I;
				if (alpha_b == 0)
					break;
				BackwardPass();
				Change_b(true);
				DirectPass();
			}
		}

		public double[] Answer(double[][] enter)
		{
			x = new double[n + 1][];
			for (int j = 0; j < n; j++)
				if (j < enter.Length)
					x[j + 1] = enter[j];
				else
					x[j + 1] = new double[enter[0].Length];
			DirectPass();
			return y;
		}


		public void WriteWeights(StreamWriter sw)
		{
			StringBuilder sb = new StringBuilder("");
			sb.AppendLine("n r s m").AppendFormat("{0} {1} {2} {3}", n, r, s, m).AppendLine();
			sb.AppendLine("U");
			for (int i = 0; i < U.Length; i++)
			{
				for (int j = 0; j < U[0].Length; j++)
					sb.AppendFormat("{0} ", U[i][j]);
				sb.AppendLine();
			}
			sb.AppendLine("V");
			for (int i = 0; i < V.Length; i++)
			{
				for (int j = 0; j < V[0].Length; j++)
					sb.AppendFormat("{0} ", V[i][j]);
				sb.AppendLine();
			}
			sb.AppendLine("W");
			for (int i = 0; i < W.Length; i++)
			{
				for (int j = 0; j < W[0].Length; j++)
					sb.AppendFormat("{0} ", W[i][j]);
				sb.AppendLine();
			}
			sb.AppendLine("a");
			for (int i = 0; i < a.Length; i++)
				sb.AppendFormat("{0} ", a[i]);
			sb.AppendLine();
			
			sb.AppendLine("b");
			for (int i = 0; i < b.Length; i++)
				sb.AppendFormat("{0} ", b[i]);
			sb.AppendLine();

			sw.Write(sb.ToString());
		}

		public RecurrentNeuronet(StreamReader sr)
		{
			string tmp = sr.ReadLine();
			if (tmp != "n r s m")
				throw new Exception("error");
			tmp = sr.ReadLine();
			string[] arr = tmp.Split(' ');
			n = int.Parse(arr[0]);
			r = int.Parse(arr[1]);
			s = int.Parse(arr[2]);
			m = int.Parse(arr[3]);

			tmp = sr.ReadLine();
			if (tmp != "U")
				throw new Exception("error");
			U = new double[r][];
			for (int i = 0; i < r; i++)
			{
				U[i] = new double[r];
				tmp = sr.ReadLine();
				arr = tmp.Split(' ');
				for (int j = 0; j < r; j++)
					U[i][j] = double.Parse(arr[j]);
			}

			tmp = sr.ReadLine();
			if (tmp != "V")
				throw new Exception("error");
			V = new double[r][];
			for (int i = 0; i < r; i++)
			{
				V[i] = new double[s];
				tmp = sr.ReadLine();
				arr = tmp.Split(' ');
				for (int j = 0; j < s; j++)
					V[i][j] = double.Parse(arr[j]);
			}

			tmp = sr.ReadLine();
			if (tmp != "W")
				throw new Exception("error");
			W = new double[m][];
			for (int i = 0; i < m; i++)
			{
				W[i] = new double[r];
				tmp = sr.ReadLine();
				arr = tmp.Split(' ');
				for (int j = 0; j < r; j++)
					W[i][j] = double.Parse(arr[j]);
			}

			tmp = sr.ReadLine();
			if (tmp != "a")
				throw new Exception("error");
			a = new double[r];
			tmp = sr.ReadLine();
			arr = tmp.Split(' ');
			for (int i = 0; i < r; i++)
				a[i] = double.Parse(arr[i]);

			tmp = sr.ReadLine();
			if (tmp != "b")
				throw new Exception("error");
			b = new double[m];
			tmp = sr.ReadLine();
			arr = tmp.Split(' ');
			for (int i = 0; i < m; i++)
				b[i] = double.Parse(arr[i]);

			h = new double[n + 1][];
			state = new double[n + 1][];
			for (int j = 0; j <= n; j++)
			{
				h[j] = new double[r];
				state[j] = new double[r];
			}
			finalState = new double[m];
			y = new double[m];
			d = new double[m];
		}


		// вспомогательное
		private int MaxInnerLength(double[][][] a)
		{
			int max = a[0].Length;
			for (int i = 1; i < a.Length; i++)
				if (a[i].Length > max)
					max = a[i].Length;
			return max;
		}
	}
}
