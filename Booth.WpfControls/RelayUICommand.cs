using System;


namespace Booth.WpfControls
{
    public class RelayUICommand : RelayCommand
    {
        public string Text { get; set; }

        public RelayUICommand(string text, Action methodToExecute, Func<bool> canExecuteEvaluator)
            : base(methodToExecute, canExecuteEvaluator)
        {
            Text = text;
        }

        public RelayUICommand(string text, Action methodToExecute)
            : base(methodToExecute)
        {
            Text = text;
        }
    }

    public class RelayUICommand<T> : RelayCommand<T>
    {
        public string Text { get; set; }

        public RelayUICommand(string text, Action<T> methodToExecute, Func<bool> canExecuteEvaluator)
            : base(methodToExecute, canExecuteEvaluator)
        {
            Text = text;
        }

        public RelayUICommand(string text, Action<T> methodToExecute)
            : base(methodToExecute)
        {
            Text = text;
        }
    }
}
