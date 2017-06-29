using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Models
{
	public interface IUIModel
	{
		object Clone();
		void UpdateBy(object other);
	}
}
