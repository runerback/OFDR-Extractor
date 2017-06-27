using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Extractor.Business
{
	public static class ConfigManager
	{
		static ConfigManager()
		{
			if (!checkOFDRRoot())
			{

			}
		}

		private static bool checkOFDRRoot()
		{
			var ofdrRoot = Properties.Settings.Default["OFDRRoot"] as string;
			var ofdrRootDir = new DirectoryInfo(ofdrRoot);
			if (ofdrRootDir.Exists)
			{
				//var ofdrMain = ofdrRootDir.GetFiles("OFDR.exe", SearchOption.TopDirectoryOnly);
				//if (ofdrMain != null && ofdrMain.Length == 1)
				//{
				//}
				_OFDRRoot = ofdrRootDir.FullName;
				return true;
			}
			return false;
		}

		private static string _OFDRRoot;
		public static string OFDRRoot
		{
			get { return _OFDRRoot; }
			set
			{
				if (value != _OFDRRoot)
				{
					Properties.Settings.Default["OFDRRoot"] = value;
					_OFDRRoot = value;
				}
			}
		}
	}
}
