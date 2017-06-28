using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Models
{
	public interface IUIModel<T>
	{
		T Clone();
		void UpdateBy(T other);
	}
}
