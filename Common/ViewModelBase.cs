using System;

namespace Extractor.Common
{
	public class ViewModelBase : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				try
				{
					this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
				}
				catch
				{
					throw;
				}
			}
		}

		public void NotifyPropertyChangedAsync(string propertyName)
		{
			if (System.Windows.Application.Current != null)
			{
				System.Windows.Application.Current.Dispatcher.BeginInvoke(
					(Action<string>)this.NotifyPropertyChanged,
					System.Windows.Threading.DispatcherPriority.Render,
					propertyName);
			}
		}
	}
}
