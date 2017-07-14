using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Common.Validation
{
	public class ValidationStringModel : ValidationModelBase<string>
	{
		protected override bool convert(string value, out string result)
		{
			result = value;
			return true;
		}
	}
}
