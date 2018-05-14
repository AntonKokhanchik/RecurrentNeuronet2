using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurrentNeuronet2
{
	interface IEncoder
	{
		double[][][] EncodeText(string[][] text);
		double[][] EncodeString(string s);
	}
}
