using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Reminder.Commands;
using WPF_Reminder.Models;

namespace WPF_Reminder.ViewModels
{
    internal class UserViewModel
    {

        private User _User;
        /// <summary>
        /// Gets the UpdateCommand for the ViewModel
        /// </summary>
        public ICommand CheckCommand { get; set; }

        /// <summary>
        /// Gets the User instance
        /// </summary>
        public User User
        {
            get
            {  return _User; }
        }

        public UserViewModel()
        {
            _User = new User("Mona");
            CheckCommand = new Command_User(this);
        }

        /// <summary>
        /// gets a boolean value to show whether User textbox has input or not
        /// </summary>
        public bool IsFull
        {
            get
            {
                if (User == null)
                {
                    return false;
                }
                return !String.IsNullOrWhiteSpace(User.Name);
            }
        }   

        /// <summary>
        /// Saves changes made to the User instance
        /// </summary>
        public void SaveChanges()
        {
            Debug.Assert(false, String.Format("{0} was updated.", User.Name));
        }

    }
}
