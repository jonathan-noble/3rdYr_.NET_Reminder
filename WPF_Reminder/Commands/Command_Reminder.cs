using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_Reminder.Commands
{
       
    /// <summary>
    /// A separate class that implements the ICommand interface methods for event handling purposes
    /// </summary>
    public class Command_Reminder : ICommand
    {
        private Action<object> executeMethod;   //delegate that performs action
        private Func<object, bool> canexecuteMethod;

        public Command_Reminder(Action<object> executeMethod, Func<object, bool> canexecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canexecuteMethod = canexecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executeMethod(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
