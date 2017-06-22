using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Extractor.Presentation.Controller
{
	public class PageController
	{
		public PageController()
		{
			this.operationsView = new View.OperationsView();
			this.operationsViewModel = new ViewModel.OperationsViewModel();
			this.operationsView.DataContext = this.operationsViewModel;

			this.fileMapManagerView = new View.FileMapManagerView();
			this.fileMapManagerViewModel = new ViewModel.FileMapManagerViewModel();
			this.fileMapManagerView.DataContext = this.fileMapManagerViewModel;

			this.statusView = new Controls.StatusPresenter();
		}

		private View.OperationsView operationsView;
		public FrameworkElement OperationsView
		{
			get { return this.operationsView; }
		}
		private ViewModel.OperationsViewModel operationsViewModel;

		private View.FileMapManagerView fileMapManagerView;
		public FrameworkElement FileMapManagerView
		{
			get { return this.fileMapManagerView; }
		}
		private ViewModel.FileMapManagerViewModel fileMapManagerViewModel;

		private Controls.StatusPresenter statusView;
		public FrameworkElement StatusView
		{
			get { return this.statusView; }
		}
	}
}
