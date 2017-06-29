using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Extractor
{
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			if (!Business.ConfigManager.CheckConfigurations())
			{
				Application.Current.Dispatcher.InvokeShutdown();
				return;
			}

			new MainWindow().Show();
		}

		private void onDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			Business.ExceptionManager.ShowPopup(e.Exception);
			e.Handled = true;
		}
	}
}
