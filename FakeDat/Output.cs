using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FakeDat
{
	internal static class Output
	{
		private static IList<string> data;

		static Output()
		{
			data = new List<string>();

			using (var reader = new StreamReader("win_000.nfs"))
			{
				while (!reader.EndOfStream)
				{
					data.Add(reader.ReadLine());
				}
			}
		}

		public static void Generate(ParamResolver.Result param)
		{
			if (param == null)
			{
				printDatas();
			}
			else
			{
				long size;
				if (searchAndPrint(param, out size))
				{
					//delay by size to simulate outputing file
					//int delay = (int)(size / (10 ^ 7));
					//System.Threading.Thread.Sleep(delay);

					using (var writer = File.CreateText(
						string.Format("{0}{1}", param.Name, param.Index == 0 ? null : param.Index.ToString())))
					{
						writer.WriteLine("Name: {0}", param.Name);
						writer.WriteLine("Size: {0}", size);
						writer.WriteLine("Index: {0}", param.Index);
						writer.Flush();
					}
				}
			}
		}

		private static void printDatas()
		{
			foreach (var line in data)
			{
				Console.WriteLine(line);
			}
		}

		private static bool searchAndPrint(ParamResolver.Result param, out long size)
		{
			size = 0;

			Regex reg = new Regex(string.Format(@"^{0}\s+(?<size>\d+)$", param.Name));
			int matchedCount = 0;
			foreach (var line in data)
			{
				if (reg.IsMatch(line))
				{
					if (matchedCount == param.Index)
					{
						var match = reg.Match(line);
						if (long.TryParse(match.Groups["size"].Value, out size) && size > 0)
						{
							Console.WriteLine("{0}{1}", param.Name, param.Index == 0 ? null : param.Index.ToString());
							return true;
						}
						break;
					}
					matchedCount++;
				}
			}
			Console.WriteLine("File No Found with name \"{0}\" and index {1}", param.Name, param.Index);
			return false;
		}
	}
}
