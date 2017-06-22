using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeDat
{
	public static class ParamResolver
	{
		public static Result Resolve(string[] args)
		{
			if (args != null && args.Length > 0)
			{
				if (args.Length == 1)
				{
					return new Result(args[0]);
				}
				else if (args.Length == 2)
				{
					int index = -1;
					if (int.TryParse(args[1], out index))
					{
						if (index >= 0)
						{
							return new Result(args[0], index);
						}
					}
				}
			}
			return null;
		}

		public class Result
		{
			public Result(string filename)
			{
				this.name = filename;
			}

			public Result(string filename, int index)
				: this(filename)
			{
				this.index = index;
			}

			private string name;
			public string Name { get { return name; } }

			private int index;
			public int Index { get { return index; } }
		}
	}
}
