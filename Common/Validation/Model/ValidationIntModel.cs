using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Common.Validation
{
	public class ValidationIntModel : ValidationModelBase<int>
	{
		protected override bool convert(string value, out int result)
		{
			return !int.TryParse(value, out result);
		}
	}
}
