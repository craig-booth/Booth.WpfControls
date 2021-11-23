using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Booth.WpfControls
{
    public class DialogCommand
    {
        public string Label { get; set; }
        public ICommand Command { get; set; }
        public object? CommandParameter { get; set; }
        public bool IsDefault { get; set; }
        public bool IsCancel { get; set; }

        public DialogCommand(string label, ICommand command)
            : this(label, command, null)
        {
            IsDefault = false;
            IsCancel = false;
        }

        public DialogCommand(string label, ICommand command, object? commandParameter)
        {
            Label = label;
            Command = command;
            CommandParameter = commandParameter;
        }
    }
}
