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

namespace Extractor.Controls
{
	/// <summary>
	/// OperationControlBox.xaml 的交互逻辑
	/// </summary>
	public partial class OperationControlBox : UserControl
	{
		public OperationControlBox()
		{
			InitializeComponent();

			this.updateBtnsState(Models.OperationState.Stopped);
			this.initializeBtns();
		}

		#region state
		public Models.OperationState State
		{
			get { return (Models.OperationState)GetValue(StateProperty); }
			set { SetValue(StateProperty, value); }
		}

		public static readonly DependencyProperty StateProperty =
			DependencyProperty.Register(
				"State",
				typeof(Models.OperationState),
				typeof(OperationControlBox),
				new UIPropertyMetadata(Models.OperationState.Stopped, onStatePropertyChanged));

		private static void onStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			try
			{
				if (e.NewValue != null)
				{
					(d as OperationControlBox).updateBtnsState((Models.OperationState)e.NewValue);
				}
			}
			catch
			{
				throw;
			}
		}
		#endregion state

		#region isPaused
		public bool IsPaused
		{
			get { return (bool)GetValue(IsPausedProperty); }
		}

		private void setIsPaused(bool value)
		{
			if (value != this.IsPaused)
			{
				this.SetValue(IsPausedPropertyKey, value);
			}
		}

		internal static readonly DependencyPropertyKey IsPausedPropertyKey =
			DependencyProperty.RegisterReadOnly(
				"IsPaused",
				typeof(bool),
				typeof(OperationControlBox),
				new UIPropertyMetadata(false));

		public static readonly DependencyProperty IsPausedProperty = IsPausedPropertyKey.DependencyProperty;
		#endregion isPaused

		#region btns
		private void initializeBtns()
		{
			this.startBtn.Click += this.onStart;
			this.pauseBtn.Click += this.onPause;
			this.stopBtn.Click += this.onStop;
		}

		private void onStart(object sender, RoutedEventArgs e)
		{
			try
			{
				this.State = Models.OperationState.Running;
			}
			catch
			{
				throw;
			}
		}

		private void onPause(object sender, RoutedEventArgs e)
		{
			try
			{
				this.State = Models.OperationState.Paused;
			}
			catch
			{
				throw;
			}
		}

		private void onStop(object sender, RoutedEventArgs e)
		{
			try
			{
				this.State = Models.OperationState.Stopped;
			}
			catch
			{
				throw;
			}
		}

		private void updateBtnsState(Models.OperationState state)
		{
			try
			{
				switch (state)
				{
					case Models.OperationState.Running:
						{
							this.startBtn.Visibility = System.Windows.Visibility.Hidden;
							this.pauseBtn.Visibility = System.Windows.Visibility.Visible;
							this.stopBtn.IsEnabled = true;
							this.setIsPaused(false);
						}
						break;
					case Models.OperationState.Paused:
						{
							this.startBtn.Visibility = System.Windows.Visibility.Visible;
							this.pauseBtn.Visibility = System.Windows.Visibility.Hidden;
							this.stopBtn.IsEnabled = true;
							this.setIsPaused(true);
						}
						break;
					case Models.OperationState.Stopped:
						{
							this.startBtn.Visibility = System.Windows.Visibility.Visible;
							this.pauseBtn.Visibility = System.Windows.Visibility.Hidden;
							this.stopBtn.IsEnabled = false;
							this.setIsPaused(false);
						}
						break;
					default: throw new NotImplementedException(state.ToString());
				}
			}
			catch
			{
				throw;
			}
		}
		#endregion btns
	}
}
