using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Extractor.Business
{
	public static class ExceptionManager
	{
		public static void ShowPopup(Exception e)
		{
			if (e == null) { return; }

			if (e != null)
			{
				if (e is AggregateException)
				{
					var ae = e as AggregateException;
					if (ae.InnerExceptions != null)
					{
						foreach (var innerException in ae.InnerExceptions)
						{
							ShowPopup(innerException);
						}
					}
				}
				else if (e is System.Reflection.TargetInvocationException)
				{
					var tie = e as System.Reflection.TargetInvocationException;
					ShowPopup(tie.InnerException);
				}
				else
				{
					Exception inner = e;
					while (inner != null)
					{
						MessageBox.Show(inner.ToString(), inner.Message);
						inner = inner.InnerException;
					}
				}
			}
		}

		public static void NewTask(Action action)
		{
			Task.Factory.StartNew(action).createExceptionHandler();
		}

		public static void NewTask(Action<object> action, object state)
		{
			Task.Factory.StartNew(action, state).createExceptionHandler();
		}

		private static void createExceptionHandler(this Task task)
		{
			task.ContinueWith(t =>
			{
				if (t.Exception != null)
				{
					ShowPopup(t.Exception);
				}
			},  TaskContinuationOptions.OnlyOnFaulted);
		}
	}
}
