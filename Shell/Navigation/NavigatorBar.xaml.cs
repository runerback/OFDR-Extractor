using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Extractor.Shell.Navigation
{
	/// <summary>
	/// Interaction logic for Navigator.xaml
	/// </summary>
	public partial class NavigatorBar : UserControl
	{
		public NavigatorBar()
		{
			InitializeComponent();

			DataContext = PageNavigator.Business.ViewModels.NavigatorViewModel;
		}
	}
}
