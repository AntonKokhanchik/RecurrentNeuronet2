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

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialogInput.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string[][] text = getWords();
            encoder = new SumEncoder(text);
			encodedText = encoder.EncodeText(text);
			label2.Enabled = true;
			label3.Enabled = true;
			label5.Enabled = true;
			label6.Enabled = true;
			textBoxInnerLength.Enabled = true;
			textBoxEpsilon.Enabled = true;
			textBoxAlpha.Enabled = true;
			buttonLearn.Enabled = true;
			textBoxTime.Enabled = true;
		}

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

		private void buttonLearn_Click(object sender, EventArgs e)
		{
			neuronet = new RecurrentNeuronet(encodedText, 
				Int32.Parse(textBoxInnerLength.Value.ToString()), double.Parse(textBoxEpsilon.Text), 
				double.Parse(textBoxAlpha.Text), int.Parse(textBoxTime.Text));
		}

		private void buttonAnswer_Click(object sender, EventArgs e)
		{
			textBoxAnswer.Text = String.Concat(neuronet.Answer(encoder.EncodeString(textBoxString.Text)));
		}

		private void buttonExitFile_Click(object sender, EventArgs e)
		{
			saveFileDialogResults.ShowDialog();
		}

		private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			using (StreamWriter sw = new StreamWriter(saveFileDialogResults.OpenFile()))
			{
				double[][] answers = new double[encodedText.Length][];
				StringBuilder sb = new StringBuilder("\t");
				for (int i = 0; i < encodedText.Length; i++)
				{
					answers[i] = (double[])neuronet.Answer(encodedText[i]).Clone();
					sb.AppendFormat("{0}\t", i+1);
				}
				sw.WriteLine(sb.ToString());
				for (int i = 0; i < encodedText.Length; i++)
				{
					sb = new StringBuilder((i+1).ToString());
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

		private void buttonSaveDebug_Click(object sender, EventArgs e)
		{
			saveFileDialogLog.ShowDialog();
		}

		private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
		{
			using (StreamWriter sw = new StreamWriter(saveFileDialogLog.OpenFile()))
			{
				sw.Write(neuronet.log.ToString());
			}
		}

		private void buttonSaveNet_Click(object sender, EventArgs e)
		{
			saveFileDialogNet.ShowDialog();
		}

		private void saveFileDialogNet_FileOk(object sender, CancelEventArgs e)
		{
			using (StreamWriter sw = new StreamWriter(saveFileDialogNet.OpenFile()))
			{
				neuronet.WriteWeights(sw);
			}
		}

		private void buttonLoadNet_Click(object sender, EventArgs e)
		{
			openFileDialogLoadNet.ShowDialog();
		}

		private void openFileDialogLoadNet_FileOk(object sender, CancelEventArgs e)
		{
			using (StreamReader sr = new StreamReader(openFileDialogLoadNet.OpenFile(), Encoding.UTF8))
			{
				neuronet = new RecurrentNeuronet(sr);
			}
		}
	}
}
