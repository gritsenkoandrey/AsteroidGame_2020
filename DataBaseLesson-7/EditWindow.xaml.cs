using System.Data;
using System.Windows;

namespace DataBaseLesson_7
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public DataRow resultRow { get; set; }
        public EditWindow(DataRow dataRow)
        {
            InitializeComponent();
            resultRow = dataRow;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fIOTextBox.Text = resultRow["FIO"].ToString();
            birthdayTextBox.Text = resultRow["Birthday"].ToString();
            emailTextBox.Text = resultRow["Email"].ToString();
            phoneTextBox.Text = resultRow["Phone"].ToString();
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            resultRow["FIO"] = fIOTextBox.Text;
            resultRow["Birthday"] = birthdayTextBox.Text;
            resultRow["Email"] = emailTextBox.Text;
            resultRow["Phone"] = phoneTextBox.Text;
            DialogResult = true;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}