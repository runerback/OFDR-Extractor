using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FakeDat
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				var param = ParamResolver.Resolve(args);
                Output.Generate(param);
            }
			catch(Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
