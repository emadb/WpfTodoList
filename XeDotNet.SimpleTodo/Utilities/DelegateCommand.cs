using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Trilance.Trimp.Common
{
	public class DelegateCommand<T> : ICommand
	{
		private readonly Action<T> _executeMethod = null;
		private readonly Func<T, bool> _canExecuteMethod = null;
		private readonly Dispatcher _dispatcher;

		public DelegateCommand(Action<T> executeMethod)
			: this(executeMethod, null)
		{
		}

		public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
		{
			if (executeMethod == null && canExecuteMethod == null)
				throw new ArgumentNullException("executeMethod", "DelegateCommandDelegatesCannotBeNull");

			_executeMethod = executeMethod;
			_canExecuteMethod = canExecuteMethod;
			if (Application.Current != null)
			{
				_dispatcher = Application.Current.Dispatcher;
			}
		}

		public bool CanExecute(T parameter)
		{
			if (_canExecuteMethod == null) return true;
			return _canExecuteMethod(parameter);
		}

		public void Execute(T parameter)
		{
			if (_executeMethod == null) return;
			_executeMethod(parameter);
		}

		bool ICommand.CanExecute(object parameter)
		{
			// HACK: la prima volta arriva null
			if (parameter == null)
				return true;
			return CanExecute((T)parameter);
		}

		public event EventHandler CanExecuteChanged;

		void ICommand.Execute(object parameter)
		{
			Execute((T)parameter);
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
		public void RaiseCanExecuteChanged()
		{
			OnCanExecuteChanged();
		}

		protected virtual void OnCanExecuteChanged()
		{
			EventHandler canExecuteChangedHandler = CanExecuteChanged;
			if (canExecuteChangedHandler != null)
			{
				if (_dispatcher != null && !_dispatcher.CheckAccess())
				{
					_dispatcher.BeginInvoke(DispatcherPriority.Normal,
					                        (Action)OnCanExecuteChanged);
				}
				else
				{
					canExecuteChangedHandler(this, EventArgs.Empty);
				}
			}
		}

	}

	public class DelegateCommand : ICommand
	{
		private readonly Action _executeMethod = null;
		private readonly Func<bool> _canExecuteMethod = null;
		private readonly Dispatcher _dispatcher;

		public DelegateCommand(Action executeMethod)
			: this(executeMethod, null)
		{
		}

		public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
		{
			if (executeMethod == null && canExecuteMethod == null)
				throw new ArgumentNullException("executeMethod", "DelegateCommandDelegatesCannotBeNull");

			this._executeMethod = executeMethod;
			this._canExecuteMethod = canExecuteMethod;
			if (Application.Current != null)
			{
				_dispatcher = Application.Current.Dispatcher;
			}
		}

		public bool CanExecute()
		{
			if (_canExecuteMethod == null) return true;
			return _canExecuteMethod();
		}

		public void Execute()
		{
			if (_executeMethod == null) return;
			_executeMethod();
		}

		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute();
		}

		public event EventHandler CanExecuteChanged;

		void ICommand.Execute(object parameter)
		{
			Execute();
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
		public void RaiseCanExecuteChanged()
		{
			OnCanExecuteChanged();
		}

		protected virtual void OnCanExecuteChanged()
		{
			EventHandler canExecuteChangedHandler = CanExecuteChanged;
			if (canExecuteChangedHandler != null)
			{
				if (_dispatcher != null && !_dispatcher.CheckAccess())
				{
					_dispatcher.BeginInvoke(DispatcherPriority.Normal,
					                        (Action)OnCanExecuteChanged);
				}
				else
				{
					canExecuteChangedHandler(this, EventArgs.Empty);
				}
			}
		}
	}
}