using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Reminder.ViewModels;

namespace WPF_Reminder.Commands
{

    internal class Command_User : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the UpdateCustomerCommand class.
        /// </summary>
        public Command_User(UserViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        private UserViewModel _ViewModel;

        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _ViewModel.IsFull;
        }

        public void Execute(object parameter)
        {
            _ViewModel.SaveChanges();
        }
    }
}
