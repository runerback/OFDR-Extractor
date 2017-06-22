using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Extractor.Presentation.ViewModel
{
	public class StatusViewModel : Common.ViewModelBase
	{
		public StatusViewModel()
		{
			Business.ProgressManager.Instance.InProgressStateChanged += this.onProgressManagerInProgressStateChanged;
		}

		public void onProgressManagerInProgressStateChanged(bool value)
		{
			this.progressBarVisibility = value ? Visibility.Visible : Visibility.Collapsed;
			this.NotifyPropertyChanged("ProgressBarVisibility");
		}

		private Visibility progressBarVisibility = Visibility.Collapsed;
		public Visibility ProgressBarVisibility
		{
			get { return progressBarVisibility; }
		}
	}
}
