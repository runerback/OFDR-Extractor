using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Controllers
{
	public class HomePageController : PageNavigator.Business.ModuleControllerBase
	{
		public HomePageController(PageNavigator.Model.ModuleData moduleData)
			: base(moduleData)
		{
			this.isHomePage = true;
		}

		protected override System.Windows.FrameworkElement setStartPage()
		{
			var v = new Shell.View.FileMapManagerView();
			v.DataContext = new Presentation.ViewModel.FileMapManagerViewModel();
			return v;
		}
	}
}
