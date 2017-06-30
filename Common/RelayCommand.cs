using System;

namespace Extractor.Common
{
	public class RelayCommand : System.Windows.Input.ICommand
	{
		private readonly Action<object> execute;
		private readonly Predicate<object> canExecute;
		#region Constructors

		public RelayCommand(Action<object> execute)
			: this(execute, null) { }

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
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


		public event EventHandler CanExecuteChanged
		{
			add { System.Windows.Input.CommandManager.RequerySuggested += value; }
			remove { System.Windows.Input.CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter)
		{
			this.execute(parameter);
		}

		#endregion
	}
}
