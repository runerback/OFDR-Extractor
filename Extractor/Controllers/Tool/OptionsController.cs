using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Controllers.Tool
{
	public class OptionsController : PageNavigator.Business.ModuleControllerBase
	{
		public OptionsController(PageNavigator.Model.ModuleData moduleData)
			: base(moduleData)
		{
			this.isSingleMode = true;
		}

		protected override System.Windows.FrameworkElement setStartPage()
		{
			var v = new Shell.View.SettingsView();
			v.DataContext = new Presentation.ViewModel.SettingsViewModel();
			return v;
		}
	}
}
