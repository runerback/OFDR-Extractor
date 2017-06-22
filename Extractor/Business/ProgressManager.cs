using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Business
{
	public class ProgressManager : Common.ViewModelBase
	{
		protected ProgressManager() { }

		private double total = 100.0;

		private double current = 0.0;
		public double Current
		{
			get { return current; }
		}

		private bool inProgress;
		public bool InProgress
		{
			get { return this.inProgress; }
			protected set
			{
				if (value != this.inProgress)
				{
					this.inProgress = value;

					if (this.InProgressStateChanged != null)
					{
						this.InProgressStateChanged(value);
					}
				}
			}
		}

		public event Action<bool> InProgressStateChanged;

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

			current = total * percent;
			this.NotifyPropertyChangedAsync("Current");
		}

		private static ProgressManager instance = new ProgressManager();
		public static ProgressManager Instance
		{
			get { return instance; }
		}
	}
}
