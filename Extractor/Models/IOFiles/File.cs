using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Extractor.Models.IOFiles
{
	public class File : IFileInfo
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("size")]
		public long Size { get; set; }

		[XmlAttribute("index")]
		public int Index { get; set; }

		public bool ShouldSerializeIndex()
		{
			return this.Index > 0;
		}
	}
}
