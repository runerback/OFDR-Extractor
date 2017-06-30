using System.Xml.Serialization;

namespace Extractor.Data
{
	public class IOFile : IFileInfo
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
