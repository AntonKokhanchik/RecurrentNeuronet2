using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurrentNeuronet2
{
	/// <summary>
	/// Кодировка на основе частот встречаемости слов в тексте
	/// </summary>
	class FrequencyEncoder : IEncoder
	{
		private Dictionary<string, double> dictionary;

		public FrequencyEncoder(string[][] text)
		{
			dictionary = new Dictionary<string, double>();
			double wordsCount = 0;

			for (int i = 0; i < text.Length; i++)
				for (int j = 0; j < text[i].Length; j++)
				{
					if (dictionary.ContainsKey(text[i][j]))
						dictionary[text[i][j]]++;
					else
						dictionary.Add(text[i][j], 1);
					wordsCount++;
				}

			dictionary = dictionary.OrderByDescending(d => d.Value).ToDictionary(x => x.Key, x => x.Value);


			List<string> d2 = new List<string>();
			dictionary[dictionary.ElementAt(0).Key] /= wordsCount;
			for (int i = 1; i < dictionary.Count; i++)
			{
				dictionary[dictionary.ElementAt(i).Key] /= wordsCount;

				// Отменяем вероятность получения двух слов с одинаковой частотой
				if (dictionary[dictionary.ElementAt(i).Key] == dictionary[dictionary.ElementAt(i - 1).Key])
					if (d2.Count == 0)
					{
						d2.Add(dictionary.ElementAt(i - 1).Key);
						d2.Add(dictionary.ElementAt(i).Key);
					}
					else
						d2.Add(dictionary.ElementAt(i).Key);

				else if (d2.Count > 0)
				{
					for (int j = 0; j < d2.Count; j++)
						dictionary[d2.ElementAt(j)] += ((double)j / d2.Count) / wordsCount;
					d2.Clear();
				}
			}
			if (d2.Count > 0)
			{
				for (int j = 0; j < d2.Count; j++)
					dictionary[d2.ElementAt(j)] += ((double)j / d2.Count) / wordsCount;
				d2.Clear();
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
