using System.Collections.Generic;
using System.Xml.Serialization;

namespace Extractor.Data
{
	[XmlRoot("root")]
	public class IOFolder
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlElement("folder")]
		public List<IOFolder> SubFolders { get; set; }

		public bool ShouldSerializeSubFolders()
		{
			return this.SubFolders != null && this.SubFolders.Count > 0;
		}

		[XmlElement("file")]
		public List<IOFile> Files { get; set; }

		public bool ShouldSerializeFiles()
		{
			return this.Files != null && this.Files.Count > 0;
		}
	}
}
