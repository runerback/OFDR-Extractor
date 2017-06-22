using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Extractor.Business
{
	public class StatusManager : Common.ViewModelBase
	{
		protected StatusManager() { }

		private double totalProgress = 100.0;

		private double currentProgress = 0.0;
		public double CurrentProgress
		{
			get { return currentProgress; }
			private set
			{
				if (value != this.currentProgress)
				{
					this.currentProgress = value;
					this.NotifyPropertyChangedAsync("CurrentProgress");
				}
			}
		}

		private bool inProgress;
		public bool InProgress
		{
			get { return this.inProgress; }
			private set
			{
				if (value != this.inProgress)
				{
					this.inProgress = value;

					this.ProgressBarVisibility = value ? Visibility.Visible : Visibility.Hidden;
				}
			}
		}

		private Visibility progressBarVisibility = Visibility.Hidden;
		public Visibility ProgressBarVisibility
		{
			get { return progressBarVisibility; }
			private set
			{
				if (value != this.progressBarVisibility)
				{
					this.progressBarVisibility = value;
					this.NotifyPropertyChangedAsync("ProgressBarVisibility");
				}
			}
		}

		public void UpdateProgress(double percent)
		{
			double currentPercent;
			if (percent < 0.0) currentPercent = 0.0;
			else if (percent > 1.0) currentPercent = 1.0;
			else currentPercent = percent;

			if (currentPercent == 0.0 || currentPercent == 1.0)
				this.InProgress = false;
			else
				this.InProgress = true;

			this.CurrentProgress = totalProgress * percent;
		}

		private string status;
		public string Status
		{
			get { return this.status; }
			private set
			{
				if (this.status != value)
				{
					this.status = value;
					this.NotifyPropertyChangedAsync("Status");
				}
			}
		}

		public void UpdateStatus(string status)
		{
			this.Status = status;
		}

		private static StatusManager instance = new StatusManager();
		public static StatusManager Instance
		{
			get { return instance; }
		}
	}
}
