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
			this.DispatcherUnhandledException += this.onDispatcherUnhandledException;
			try
			{
				var v = new MainWindow();
				v.Show();
				
				if (!Business.ConfigManager.CheckConfigurations())
				{
					Application.Current.Dispatcher.InvokeShutdown();
					return;
				}

				Shell.Business.ModulesManager.Initialize();

				v.Content = new Shell.Framework()
				{
					DataContext = new Presentation.ViewModel.FrameworkViewModel()
				};
			}
			catch
			{
				throw;
			}
		}

		private void onDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			Business.ExceptionManager.ShowPopup(e.Exception);
			e.Handled = true;
		}
	}
}
