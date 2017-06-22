using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Common
{
	public class DelegateCommand : System.Windows.Input.ICommand
	{
		private readonly Action<object> execute;
		private readonly Predicate<object> canExecute;
		#region Constructors

		public DelegateCommand(Action<object> execute)
			: this(execute, null) { }

		public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");

			this.execute = execute;
			this.canExecute = canExecute;
		}

		#endregion

		#region ICommand Members

		public bool CanExecute(object parameter)
		{
			return this.canExecute == null ? true : this.canExecute(parameter);
		}

		public event EventHandler CanExecuteChanged;

		public void RaiseCanExecutedChanged()
		{
			if (this.CanExecuteChanged != null)
			{
				this.CanExecuteChanged(this, EventArgs.Empty);
			}
		}

		public void Execute(object parameter)
		{
			this.execute(parameter);
		}

		#endregion
	}
}
