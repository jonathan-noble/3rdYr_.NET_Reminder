using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_Reminder.ViewModels;


/// <summary>
/// @author         Jonathan Noble - noblejonathan.visualstudio.com
/// @program_name   Interaction logic for Reminder.xaml
/// @description    There is a log-in for the user;
///                 Some events are based on ICommand interface;
///                 A task notifier that uses async and await;
///                 User interaction is available to access the notes;
///                 Notes can be converted through a schedule planner View with the help of LinqToSQL;
///                 Serialization and deserialization to store the data into a binary file;
///                 Exception Handling is used to catch errors
/// </summary>



namespace WPF_Reminder
{

    /// <summary>
    /// struct for the Note which contains the Description and Note ID
    /// </summary>
    public struct Note
    {
        public Note(String desc, int noteId)
        {
            Desc = desc;
            NoteID = noteId;
        }
        public string Desc { get; set; }
        public int NoteID { get;  set; }
    }

    ///Attribute Serializable must be implemented on top of the class
    [Serializable]
    public partial class Reminder : Window
    {
        
        /// <summary>
        /// This is used to interconnect with VS and SQLExpress
        /// </summary>
        ReminderDataClassDataContext datacontext = new ReminderDataClassDataContext(Properties.Settings.Default.ReminderDatabaseConnectionString);
 
        private static List<string> _days;
        private readonly List<Note> _list;


        /// <summary>
        /// Constructor for the variables declared
        /// </summary>
        public Reminder()
        {
            InitializeComponent();
            DataContext = new ReminderViewModel();
            DataContext = new UserViewModel();

            if (datacontext.DatabaseExists()) NoteDatagrid.ItemsSource = datacontext.NoteTables;           // Connecting to the SQLExpress Server       
            _days = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            _list = new List<Note>();
        }

        /// <summary>
        /// Saves the database to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(Object sender, RoutedEventArgs e)
        {
            datacontext.SubmitChanges();
        }

        /// <summary>
        /// Adds a note to the list
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool AddNote(Note n)
        {
            if (string.IsNullOrEmpty(n.Desc)) throw new ArgumentException("Description must not be empty");

            if ((from l in _list select l.Desc).ToList().Contains(n.Desc))
            {
                return false;
            }
            _list.Add(n);
            return true;
        }


       /// <summary>
       /// Button for user log-in
       /// </summary>
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            // hides all the initial buttons with a ternary operator
            userTxt.Visibility = userTxt.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            inputTxt.Visibility = inputTxt.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            userInput.Visibility = userInput.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

            //shows the user input saved on top     
            greetingTxt.Text = "Hallo " + this.inputTxt.Text + "!"; 
        }



        private int CountList()
        {
            int count = Board.Items.Count;
            Thread.Sleep(1500);
            return count;
        }


        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            Board.Items.Add(Note.Text);

            Task<int> task = new Task<int>(CountList);
            task.Start();

            count_Txt.Text = "Processing...";
            int count = await task;
            count_Txt.Text = count.ToString() + " item(s) in the list.";


        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Board.Items.RemoveAt(Board.Items.IndexOf(Board.SelectedItem));

           if (Board.Items.IndexOf(Board) > 0) throw new Exception("Error!Try selecting an item from the list.");
            // EXCEPTION - if item is removed with less than 0, it should return an error
        }



        private void Day_Click(object sender, RoutedEventArgs e)
        {

            foreach (var day in SelectDays("s", _days))
            {
                MessageBox.Show(day);
            }

        }

        public List<string> SelectDays(string search, List<string> days)
        {
            var query = from day in days
                where day.Contains(search)
                select day;
            return query.ToList();
        }

        public List<string> GetDays()
        {
            return _days;
        }


        /// <summary>
        /// The rest of the textboxes are implemented here
        /// </summary>

        private void NoteDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void Board_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
        private void Note_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


    }


}
