﻿
namespace Extractor.Data
{
	public interface IFileInfo
	{
		string Name { get; set; }
		long Size { get; set; }
		int Index { get; set; }
	}
}
