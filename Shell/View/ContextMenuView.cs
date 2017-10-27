using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Extractor.Shell.View
{
	public class ContextMenuView : ContextMenu
	{
		public ContextMenuView()
		{
			this.SetResourceReference(StyleProperty, typeof(ContextMenu));
		}

		protected override void OnInitialized(EventArgs e)
		{
			this.SetBinding(ItemsSourceProperty, new Binding("MenuItems") { Mode = BindingMode.OneWay });

			//this.ItemTemplate = createMenuItemTemplate();
			//this.Resources.Add(typeof(MenuItem), getBoundMenuItemStyle());

			base.OnInitialized(e);
		}

		/*
		private DataTemplate createMenuItemTemplate()
		{
			HierarchicalDataTemplate hierarchicalTemplate = new HierarchicalDataTemplate(typeof(Data.MenuItemModel));
			var hierarchicalRoot = new FrameworkElementFactory(typeof(TextBlock));
			hierarchicalRoot.SetBinding(
				TextBlock.TextProperty,
				new Binding("Title") { Mode = BindingMode.OneWay });
			hierarchicalTemplate.ItemsSource = new Binding("SubMenuItems") { Mode = BindingMode.OneWay };
			hierarchicalTemplate.VisualTree = hierarchicalRoot;

			DataTemplate template = new DataTemplate(typeof(Data.MenuItemModel));
			var templateRoot = new FrameworkElementFactory(typeof(TextBlock));
			templateRoot.SetBinding(
				TextBlock.TextProperty,
				new Binding("Title") { Mode = BindingMode.OneWay });
			template.VisualTree = templateRoot;

			hierarchicalTemplate.ItemTemplate = template;
			
			return hierarchicalTemplate;
		}
		*/

		private Style getBoundMenuItemStyle()
		{
			Style style = new Style(typeof(MenuItem), (Style)FindResource(typeof(MenuItem)));

			style.Setters.Add(new Setter(
					MenuItem.HeaderProperty,
					new Binding("Title") { Mode = BindingMode.OneWay }));

			style.Setters.Add(new Setter(
				MenuItem.CommandProperty,
				new Binding("Command") { Mode = BindingMode.OneWay }));

			style.Setters.Add(new Setter(
				MenuItem.ItemsSourceProperty,
				new Binding("SubMenuItems") { Mode = BindingMode.OneWay }));

			return style;
		}
	}
}
