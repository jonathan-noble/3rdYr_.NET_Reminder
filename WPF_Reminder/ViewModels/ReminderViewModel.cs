using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Input;
using WPF_Reminder.Commands;

namespace WPF_Reminder.ViewModels
{
    public class ReminderViewModel
    {
        /// <summary>
        /// Gets the Command_Reminder for the ViewModel
        /// </summary>
        public ICommand SerialCommand { get; set; }
        public ICommand DeserialCommand { get; set; }

        public ReminderViewModel()
        {
            SerialCommand = new Command_Reminder(SerializeMethod, canExecuteMethod);
            DeserialCommand = new Command_Reminder(DeserializeMethod, canExecuteMethod);
        }

        private bool canExecuteMethod(object parameter)
        {
            return true;
        }


        /// <summary>
        /// Serialization
        /// </summary>
        private void SerializeMethod(object parameter)
        {

            Reminder login = new Reminder();
            //creating a BF object to serialize our object
            BinaryFormatter bf = new BinaryFormatter();

            FileStream fsout = new FileStream("credentials.binary", FileMode.Create, FileAccess.Write, FileShare.None);
            try
            {
                using (fsout)
                {
                    bf.Serialize(fsout, login);
                    MessageBox.Show("Serialization successful!");
                }
            }
            catch
            {
                MessageBox.Show("An error has occured.");
            }

        }
    
        /// <summary>
        /// Deserialization
        /// </summary>
        /// <param name="parameter"></param>
         private void DeserializeMethod(object parameter)
        {
            MessageBox.Show("Deserialization on the process...");

                Reminder login = new Reminder();

                BinaryFormatter bf = new BinaryFormatter();

                FileStream fsin = new FileStream("credentials.binary", FileMode.Open, FileAccess.Read, FileShare.None);
                try
                {
                    using (fsin)
                    {
                        login = (Reminder)bf.Deserialize(fsin);
                        MessageBox.Show("Serialization successful!");
                        //serial_Txt.Text = UserName;
                }
                }
                catch
                {
                  MessageBox.Show("An error has occured.");
                }

            }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
 }
