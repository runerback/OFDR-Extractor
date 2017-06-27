using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Extractor.Models.IOFiles
{
	[XmlRoot("root")]
	public class Folder
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlElement("folder")]
		public List<Folder> SubFolders { get; set; }

		public bool ShouldSerializeSubFolders()
		{
			return this.SubFolders != null && this.SubFolders.Count > 0;
		}

		[XmlElement("file")]
		public List<File> Files { get; set; }

		public bool ShouldSerializeFiles()
		{
			return this.Files != null && this.Files.Count > 0;
		}
	}
}
