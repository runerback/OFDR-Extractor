using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Presentation.ViewModel
{
	public class FrameworkViewModel : Common.ViewModelBase
	{
		private object statusBarDataContext = Business.StatusManager.Instance;
		public object StatusBarDataContext
		{
			get { return this.statusBarDataContext; }
		}
	}
}
