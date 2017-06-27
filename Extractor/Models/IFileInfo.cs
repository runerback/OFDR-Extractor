using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Models
{
	public interface IFileInfo
	{
		string Name { get; set; }
		long Size { get; set; }
		int Index { get; set; }
	}
}
