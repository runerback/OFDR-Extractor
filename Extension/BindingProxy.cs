using System.Windows;

namespace Extractor.Extension
{
	public class BindingProxy : Freezable
	{
		protected override Freezable CreateInstanceCore()
		{
			return new BindingProxy();
		}

		public object Data
		{
			get { return (object)this.GetValue(DataProperty); }
			set { this.SetValue(DataProperty, value); }
		}

		public static readonly DependencyProperty DataProperty =
			DependencyProperty.Register(
				"Data",
				typeof(object),
				typeof(BindingProxy),
				new UIPropertyMetadata(null));
	}
}
