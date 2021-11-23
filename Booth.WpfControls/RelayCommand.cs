using System;
using System.Windows.Input;

namespace Booth.WpfControls
{

    public class RelayCommand : ICommand
    {
        private Action _MethodToExecute;
        private Func<bool> _CanExecuteEvaluator;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this._MethodToExecute = methodToExecute;
            this._CanExecuteEvaluator = canExecuteEvaluator;
        }

        public RelayCommand(Action methodToExecute)
            : this(methodToExecute, () => true)
        {
        }

        public bool CanExecute(object? paramater)
        {
            bool result = this._CanExecuteEvaluator.Invoke();
            return result;
        }

        public void Execute(object? Parameter)
        {
            this._MethodToExecute.Invoke();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private Action<T> _MethodToExecute;
        private Func<bool> _CanExecuteEvaluator;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this._MethodToExecute = methodToExecute;
            this._CanExecuteEvaluator = canExecuteEvaluator;
        }

        public RelayCommand(Action<T> methodToExecute)
            : this(methodToExecute, () => true)
        {
        }

        public bool CanExecute(object? paramater)
        {
            bool result = this._CanExecuteEvaluator.Invoke();
            return result;
        }

        public void Execute(object? parameter)
        {
            if (parameter != null)
                this._MethodToExecute.Invoke((T)parameter);
        }
    }
}
