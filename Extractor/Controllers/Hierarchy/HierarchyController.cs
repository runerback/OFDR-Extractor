using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Controllers
{
	public class HierarchyController : PageNavigator.Business.ModuleControllerBase
	{
		public HierarchyController(PageNavigator.Model.ModuleData moduleData)
			: base(moduleData)
		{
			
		}

		protected override System.Windows.FrameworkElement setStartPage()
		{
			var v = new Shell.View.FileMapManagerView();
			v.DataContext = new Presentation.ViewModel.FileMapManagerViewModel();
			return v;
		}
	}
}
