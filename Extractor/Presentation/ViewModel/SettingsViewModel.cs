using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Presentation.ViewModel
{
	public class SettingsViewModel : Common.ViewModelBase
	{
		public SettingsViewModel()
		{
			this.currentAppSettings = Business.ConfigManager.CurrentSettingsCopy;
		}

		private Data.AppSetting currentAppSettings;
		public Data.AppSetting CurrentAppSettings
		{
			get { return this.currentAppSettings; }
		}
	}
}
