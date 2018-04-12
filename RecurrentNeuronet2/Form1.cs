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
        Encoder encoder;
        RecurrentNeuronet neuronet;
		double[][][] encodedText;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string[][] text = getWords();
            encoder = new Encoder(text);
			encodedText = encoder.EncodeText(text);
        }

        private string[][] getWords()
        {
            List<string[]> text = new List<string[]>();
            StreamReader file = new StreamReader(openFileDialog1.OpenFile(), Encoding.UTF8);
 
            string s = file.ReadLine();
            while (s != null)
            {
                text.Add(s.Split(' '));
                s = file.ReadLine();
            }

            return text.ToArray();
        }

		private void buttonLearn_Click(object sender, EventArgs e)
		{
			neuronet = new RecurrentNeuronet(encodedText, 
				Int32.Parse(textBoxInnerLength.Value.ToString()), double.Parse(textBoxEpsilon.Text), double.Parse(textBoxAlpha.Text));
		}

		private void buttonAnswer_Click(object sender, EventArgs e)
		{
			textBoxAnswer.Text = neuronet.Answer(encoder.EncodeString(textBoxString.Text)).ToString();
		}
	}
}
