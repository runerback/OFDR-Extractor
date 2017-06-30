using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Threading;

namespace Extractor.Common
{
	public class AutoInvokeObservableCollection<T> : ObservableCollection<T>
	{
		public AutoInvokeObservableCollection() : base() { }

		public AutoInvokeObservableCollection(IEnumerable<T> collection) : base(collection) { }

		public AutoInvokeObservableCollection(List<T> list) : base(list) { }

		public override event NotifyCollectionChangedEventHandler CollectionChanged;

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			var handler = this.CollectionChanged;
			if (handler != null)
			{
				foreach (NotifyCollectionChangedEventHandler invoker in handler.GetInvocationList())
				{
					DispatcherObject dispatcherObj = invoker.Target as DispatcherObject;
					if (dispatcherObj != null && dispatcherObj.Dispatcher != null)
					{
						Dispatcher dispatcher = dispatcherObj.Dispatcher;
						if (!dispatcher.CheckAccess())
						{
							dispatcher.Invoke((Action<NotifyCollectionChangedEventArgs>)this.OnCollectionChanged, DispatcherPriority.DataBind, e);
							continue;
						}
					}

					invoker.Invoke(this, e);
				}
			}
		}
	}
}
