using System;
using System.Windows.Input;

namespace DansWPFLibrary.Commands
{
    public class CommandBase : ICommand
    {
		public event EventHandler HasExecuted;

		#region Fields

		readonly Action<object> _execute;
		private readonly Action _executeNoParams;
		readonly Predicate<object> _canExecute;

		#endregion // Fields

		#region Constructors

		public CommandBase(Action<object> execute)
			: this(execute, null)
		{
		}

		public CommandBase(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");

			_execute = execute;
			_canExecute = canExecute;
		}

		/// <summary>
		/// Initializes a new instance of the RelayCommand class that 
		/// can always execute.
		/// </summary>
		/// <param name="execute">The execution logic.</param>
		/// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
		public CommandBase(Action execute)
			: this(execute, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the RelayCommand class.
		/// </summary>
		/// <param name="execute">The execution logic.</param>
		/// <param name="canExecute">The execution status logic.</param>
		/// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
		public CommandBase(Action execute, Predicate<object> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}

			_executeNoParams = execute;
			_canExecute = canExecute;
		}
		#endregion // Constructors

		#region ICommand Members

		[System.Diagnostics.DebuggerStepThrough]
		public bool CanExecute(object parameter)
		{
			return _canExecute == null ? true : _canExecute(parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter)
		{
			if (_execute != null)
				_execute(parameter);
			else if (_executeNoParams != null)
				_executeNoParams();
		}

		#endregion // ICommand Members

		protected void RaiseHasExecutedEvent()
		{
			if (HasExecuted != null)
				HasExecuted(this, new EventArgs());
		}
	}
}
