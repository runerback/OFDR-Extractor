using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Extractor.Data
{
	public class LZSSFileItem : IFileInfo
	{
		public string Name { get; set; }
		public long Size { get; set; }
		public int Index { get; set; }

		public static List<LZSSFileItem> Parse(string datRtn)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(datRtn)) { return null; }
				string[] lines = datRtn.Split(new string[] { @"\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				if (lines.Length == 0) { return null; }

				List<LZSSFileItem> result = new List<LZSSFileItem>();
				Dictionary<string, int> indexMap = new Dictionary<string, int>();
				Regex lineReg = new Regex(@"^(?<name>.*)\s+(?<size>\d+)$");

				for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
				{
					string line = lines[lineNumber];
					Match match = lineReg.Match(line);
					if (match.Success)
					{
						long size = -1;
						if (!long.TryParse(match.Groups["size"].Value, out size))
						{
							continue;
						}

						string name = match.Groups["name"].Value;

						int index;
						if (!indexMap.ContainsKey(name))
						{
							indexMap.Add(name, 0);
							index = 0;
						}
						else
						{
							index = ++indexMap[name];
						}
						result.Add(new LZSSFileItem()
						{
							Name = name,
							Size = size,
							Index = index
						});
					}
				}

				return result;
			}
			catch
			{
				throw;
			}
		}

		public override string ToString()
		{
			return this.Name;
		}
	}
}
