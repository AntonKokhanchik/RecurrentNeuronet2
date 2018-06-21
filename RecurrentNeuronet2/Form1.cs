using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecurrentNeuronet2
{
	public partial class Form1 : Form
	{
		IEncoder encoder;
		RecurrentNeuronet neuronet;
		double[][][] encodedText;

		public bool learnStopped = false;

		public delegate void CrossTread();

		public Form1()
		{
			InitializeComponent();
		}

		// выбираем файл с обучающей выборкой
		private void buttonSelectFile_Click(object sender, EventArgs e)
		{
			// вызываем диалог выбора файла
			openFileDialogInput.ShowDialog();
		}

		// выбрали файл с обучающей выборкой
		private void openFileDialogInput_FileOk(object sender, CancelEventArgs e)
		{
			// получаем массив массивов слов
			string[][] text = getWords();

			// создаём кодировщик
			encoder = new SumEncoder(text);

			// получаем закодированный текст
			encodedText = encoder.EncodeText(text);

			// включаем элементы формы
			label2.Enabled = true;
			label3.Enabled = true;
			label5.Enabled = true;
			label6.Enabled = true;
			textBoxInnerLength.Enabled = true;
			textBoxEpsilon.Enabled = true;
			textBoxAlpha.Enabled = true;
			buttonLearn.Enabled = true;
			textBoxTime.Enabled = true;
			buttonLoadNet.Enabled = true;

			buttonContinueLearn.Enabled = false;
			label4.Enabled = false;
			textBoxString.Enabled = false;
			buttonAnswer.Enabled = false;
			buttonExitFile.Enabled = false;
			buttonSaveLog.Enabled = false;
			textBoxAnswer.Enabled = false;
		}

		// начинаем обучение нейросети
		private void buttonLearn_Click(object sender, EventArgs e)
		{
			neuronet = new RecurrentNeuronet(MaxInnerLength(encodedText), Int32.Parse(textBoxInnerLength.Value.ToString()),
				encodedText[0][0].Length, encodedText.Length, double.Parse(textBoxEpsilon.Text), double.Parse(textBoxAlpha.Text));

			StartAsyncLearn();
		}

		// получаем ответ обученной нейросети на одном входе
		private void buttonAnswer_Click(object sender, EventArgs e)
		{
			textBoxAnswer.Text = String.Concat(neuronet.Answer(encoder.EncodeString(textBoxString.Text)));
		}

		// выбираем файл для выходной информации (будут представлены все входы обучающей выборки и все выходы на них)
		private void buttonExitFile_Click(object sender, EventArgs e)
		{
			saveFileDialogResults.ShowDialog();
		}

		// получаем результаты и выводим в файл
		private void saveFileDialogResults_FileOk(object sender, CancelEventArgs e)
		{
			using (StreamWriter sw = new StreamWriter(saveFileDialogResults.OpenFile()))
			{
				double[][] answers = new double[encodedText.Length][];
				StringBuilder sb = new StringBuilder("\t");

				for (int i = 0; i < encodedText.Length; i++)
				{
					answers[i] = (double[])neuronet.Answer(encodedText[i]).Clone();
					sb.AppendFormat("{0}\t", i + 1);
				}
				sw.WriteLine(sb.ToString());

				for (int i = 0; i < encodedText.Length; i++)
				{
					sb = new StringBuilder((i + 1).ToString());
					for (int j = 0; j < encodedText[i].Length; j++)
					{
						sb.AppendFormat(" \t{0}", encodedText[i][j][0]);
					}
					sw.WriteLine(sb.ToString());
					sb = new StringBuilder((i + 1).ToString());
					for (int j = 0; j < answers[0].Length; j++)
						sb.AppendFormat("\t{0:N5}", answers[i][j]);
					sw.WriteLine(sb.ToString());
				}
			}
		}

		// выбираем файл для логов
		private void buttonSaveLog_Click(object sender, EventArgs e)
		{
			saveFileDialogLog.ShowDialog();
		}

		// сохраняем лог
		private void saveFileDialogLog_FileOk(object sender, CancelEventArgs e)
		{
			using (StreamWriter sw = new StreamWriter(saveFileDialogLog.OpenFile()))
			{
				sw.Write(neuronet.log.ToString());
			}
		}

		// выбираем файл для сохранения весов
		private void buttonSaveNet_Click(object sender, EventArgs e)
		{
			saveFileDialogNet.ShowDialog();
		}

		// сохраняем веса
		private void saveFileDialogNet_FileOk(object sender, CancelEventArgs e)
		{
			using (StreamWriter sw = new StreamWriter(saveFileDialogNet.OpenFile()))
			{
				neuronet.WriteWeights(sw);
			}
		}

		// выбираем файл для загрузки весов
		private void buttonLoadNet_Click(object sender, EventArgs e)
		{
			openFileDialogLoadNet.ShowDialog();
		}

		// загружаем веса
		private void openFileDialogLoadNet_FileOk(object sender, CancelEventArgs e)
		{
			using (StreamReader sr = new StreamReader(openFileDialogLoadNet.OpenFile(), Encoding.UTF8))
			{
				neuronet = new RecurrentNeuronet(sr);
			}

			buttonContinueLearn.Enabled = true;
			label4.Enabled = true;
			textBoxString.Enabled = true;
			buttonAnswer.Enabled = true;
			buttonExitFile.Enabled = true;
			buttonSaveLog.Enabled = true;
			textBoxAnswer.Enabled = true;
		}

		// продолжаем обучение
		private void buttonContinueLearn_Click(object sender, EventArgs e)
		{
			StartAsyncLearn();
		}

		// остановка обучения
		private void buttonStop_Click(object sender, EventArgs e)
		{
			learnStopped = true;
		}


		// Запускает обучение во втором потоке
		private void StartAsyncLearn()
		{
			Task t = new Task(() =>
			{
				BeginInvoke(new CrossTread(PreLearn));
				neuronet.Learn(encodedText, int.Parse(textBoxTime.Text), StopCheck);
				BeginInvoke(new CrossTread(PostLearn));
			});
	
			t.Start();
		}
		// вспомогательные функции для AsyncLearn
		private void PreLearn()
		{
			learnStopped = false;
			buttonStop.Enabled = true;
			buttonLearn.Enabled = false;
			buttonContinueLearn.Enabled = false;
		}
		private void PostLearn()
		{
			buttonStop.Enabled = false;
			buttonLearn.Enabled = true;
			buttonContinueLearn.Enabled = true;

			buttonContinueLearn.Enabled = true;
			label4.Enabled = true;
			textBoxString.Enabled = true;
			buttonAnswer.Enabled = true;
			buttonExitFile.Enabled = true;
			buttonSaveLog.Enabled = true;
			textBoxAnswer.Enabled = true;
		}
		private bool StopCheck()
		{
			return learnStopped;
		}


		/// <summary>
		/// находим max a[i].Length
		/// </summary>
		private int MaxInnerLength(double[][][] a)
		{
			int max = a[0].Length;
			for (int i = 1; i < a.Length; i++)
				if (a[i].Length > max)
					max = a[i].Length;
			return max;
		}

		/// <summary>
		/// получаем набор предложений и слов из файла
		/// </summary>
		private string[][] getWords()
		{
			List<string[]> text = new List<string[]>();
			using (StreamReader file = new StreamReader(openFileDialogInput.OpenFile(), Encoding.UTF8))
			{
				string s = file.ReadLine();
				while (s != null)
				{
					text.Add(s.Split(' '));
					s = file.ReadLine();
				}
			}
			return text.ToArray();
		}

		
	}
}
