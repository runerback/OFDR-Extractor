using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Extractor.Controls
{
    public class ProgressPresenter : ContentControl
    {
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            ProgressBar bar = new ProgressBar()
            {
                Maximum = 100.0,
                Minimum = 100.0
            };
            BindingBase valueBinding = new Binding()
            {
                Path = new PropertyPath("Current"),
                Source = Business.ProgressManager.Instance,
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(bar, ProgressBar.ValueProperty, valueBinding);

            this.Content = bar;
        }
    }
}
