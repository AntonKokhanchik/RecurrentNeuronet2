using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		double[/*m*/] q;
		double[/*n+1*/][/*r*/] p; // множители Лагранжа
	
		double alpha; // скорость обучения
		double epsilon; // точность
		double I; // ошибка

		// Функции

		// f - функция активации скрытого слоя
		private double f(double state)
		{
			//// tanh
			//return Math.Tanh(state);
			return 1 / (1 + Math.Exp(-state));
		}

		// f1 = f' - производная f
		private double f1(double state)
		{
			//// 1/(cosh)^2
			//return 1 / Math.Pow(Math.Cosh(state), 2);
			return Math.Exp(-state) / Math.Pow(1 + Math.Exp(-state), 2);
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
			// 1/(cosh)^2
			return Math.Exp(-state) / Math.Pow(1 + Math.Exp(-state),2);
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
		private void WeightsChange()
		{
			for (int l = 0; l < m; l++)
			{
				// dW
				for (int k = 0; k < r; k++)
					W[l][k] -= alpha * q[l] * g1(finalState[l]) * h[n][k];
				
				// db
				b[l] -= alpha * q[l] * g1(finalState[l]);
			}

			for (int l = 0; l < r; l++)
			{
				// dU
				for (int k = 0; k < r; k++)
				{
					double Spf1h = 0;
					for (int t = 0; t < n; t++)
						Spf1h += p[t + 1][l] * f1(state[t + 1][l]) * h[t][k];
					U[l][k] -= alpha * Spf1h;
				}

				// dV
				for (int k = 0; k < s; k++)
				{
					double Spf1x = 0;
					for (int t = 0; t < n; t++)
						Spf1x += p[t + 1][l] * f1(state[t + 1][l]) * x[t+1][k];
					V[l][k] -= alpha * Spf1x;
				}

				// da
				double Spf1 = 0;
				for (int t = 0; t < n; t++)
					Spf1 += p[t + 1][l] * f1(state[t + 1][l]);
				a[l] -= alpha * Spf1;
			}
		}


		// Обратная связь

		public RecurrentNeuronet(double[/*строк*/][/*слов*/][/*размерность слова*/] enters,
			int innerLength, double accuracy, double step, int learnTime)
		{
			// сюда ещё можно исключений набросать, валидаторов
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			
			n = maxInnerLength(enters);
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


			bool isLearnedInThisCicle;
			do
			{
				isLearnedInThisCicle = false;
				for (int i = 0; i < m; i++)
				{
					alpha = step;
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

					Learn(ref isLearnedInThisCicle);
				}
			} while (isLearnedInThisCicle && stopwatch.Elapsed.Minutes < learnTime);
		}

		private void Learn(ref bool isLearnedInThisCicle)
		{
			bool IsRepeatNeeded;
			do
			{
				IsRepeatNeeded = false;
				DirectPass();

				double I_old = I;
				double[][] W_old = new double[m][];
				for (int j = 0; j < m; j++)
					W_old[j] = (double[])W[j].Clone();
				double[][] U_old = new double[r][];
				double[][] V_old = new double[r][];
				for (int j = 0; j < r; j++)
				{
					U_old[j] = (double[])U[j].Clone();
					V_old[j] = (double[])V[j].Clone();
				}
				double[] a_old = (double[])a.Clone();
				double[] b_old = (double[])b.Clone();

				BackwardPass();
				WeightsChange();

				isLearnedInThisCicle = true;

				DirectPass();
				if (I >= I_old)
				{
					alpha = alpha / 2;
					if (alpha == 0)
						throw new Exception("Нейросеть не может обучиться на таких данных");
					W = W_old;
					U = U_old;
					V = V_old;
					a = a_old;
					b = b_old;
					DirectPass();
					IsRepeatNeeded = true;
				}

			} while (IsRepeatNeeded);


			/*DirectPass();
			while (I > epsilon)
			{
				double I_old = I;
				// сохраняем старые веса
				double[][] W_old = new double[m][];
				for (int j = 0; j < m; j++)
					W_old[j] = (double[])W[j].Clone();
				double[][] U_old = new double[r][];
				double[][] V_old = new double[r][];
				for (int j = 0; j < r; j++)
				{
					U_old[j] = (double[])U[j].Clone();
					V_old[j] = (double[])V[j].Clone();
				}
				double[] a_old = (double[])a.Clone();
				double[] b_old = (double[])b.Clone();

				BackwardPass();
				WeightsChange();

				isLearnedInThisCicle = true;

				DirectPass();
				if (I >= I_old)
				{
					alpha = alpha / 2;
					if (alpha == 0)
						throw new Exception("Нейросеть не может обучиться на таких данных");
					W = W_old;
					U = U_old;
					V = V_old;
					a = a_old;
					b = b_old;
					DirectPass();
				}
			}*/
		}

		private void CalculateI()
		{
			I = 0;
			for (int i = 0; i < m; i++)
				I += (d[i] - y[i]) * (d[i] - y[i]);
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


		// вспомогательное
		private int maxInnerLength(double[][][] a)
		{
			int max = a[0].Length;
			for (int i = 1; i < a.Length; i++)
				if (a[i].Length > max)
					max = a[i].Length;
			return max;
		}
	}
}
