using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurrentNeuronet2
{
	/// <summary>
	/// Интерфейс кодировки входного текста
	/// </summary>
	interface IEncoder
	{
		double[][][] EncodeText(string[][] text);
		double[][] EncodeString(string s);
	}
}
