using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Controllers.Help
{
	public class AboutController : PageNavigator.Business.ModuleControllerBase
	{
		public AboutController(PageNavigator.Model.ModuleData moduleData)
			: base(moduleData)
		{
			this.isSingleMode = true;
		}

		protected override System.Windows.FrameworkElement setStartPage()
		{
			var v = new Shell.View.FileMapManagerView();
			v.DataContext = new Presentation.ViewModel.FileMapManagerViewModel();
			return v;
		}
	}
}
