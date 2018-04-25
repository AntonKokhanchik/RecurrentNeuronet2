using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurrentNeuronet2
{
	class SumEncoder
	{
		private Dictionary<string, double> dictionary;

		public SumEncoder(string[][] text)
		{
			dictionary = new Dictionary<string, double>();
			for (int i = 0; i < text.Length; i++)
				for (int j = 0; j < text[i].Length; j++)
					if (!dictionary.ContainsKey(text[i][j]))
					{
						int s = 0;
						for (int k = 0; k < text[i][j].Length; k++)
							s += (int)text[i][j][k];
						dictionary.Add(text[i][j], s);
					}
		}

		public double[][][] EncodeText(string[][] text)
		{
			double[][][] answer = new double[text.Length][][];

			for (int i = 0; i < text.Length; i++)
			{
				answer[i] = new double[text[i].Length][];
				for (int j = 0; j < text[i].Length; j++)
					answer[i][j] = new double[] { dictionary[text[i][j]] };
			}
			return answer;
		}

		public double[][] EncodeString(string s)
		{
			string[] words = s.Split(' ');
			double[][] answer = new double[words.Length][];

			for (int i = 0; i < words.Length; i++)
				answer[i] = new double[] { dictionary[words[i]] };

			return answer;
		}
	}
}
