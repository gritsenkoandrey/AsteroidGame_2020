using System.Collections.ObjectModel;
using System.Windows;
using Lesson_5_HomeWork.Work;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Windows.Controls;

namespace Lesson_5_HomeWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<WorkClass> _workClasses;

        string connectionString;
        SqlDataAdapter adapter;
        DataTable _dataTable;

        //private BindingList<WorkClass> _workClasses;
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ed_Homework.RowEditEnding += PhonesGrid_RowEditEnding;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //_workClasses = new BindingList<WorkClass>()
            string sql = "SELECT * FROM dataGrid";
            _dataTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                // установка команды на добавление для вызова хранимой процедуры
                adapter.InsertCommand = new SqlCommand("dataGrid", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@employee", SqlDbType.NVarChar, 50, "Employee"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@department", SqlDbType.NVarChar, 50, "Department"));
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;

                connection.Open();
                adapter.Fill(_dataTable);
                ed_Homework.ItemsSource = _dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            _workClasses = new ObservableCollection<WorkClass>()
            {
                new WorkClass(){ Employee = "Andrey Gritsenko", Department = "GeekBrains"},
                new WorkClass(){ Employee = "Irina Gritsenko", Department = "GeekBrains"},
                new WorkClass(){ Employee = "Arina Gritsenko", Department = "GeekBrains"}
            };
            
            ed_Homework.ItemsSource = _workClasses;
            //_workClasses.ListChanged += _workClassesChanged;
            //_workClasses.CollectionChanged += _workClasses_CollectionChanged;

        }
        //private void _workClasses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    switch (e.Action)
        //    {
        //        case NotifyCollectionChangedAction.Add:
        //            MessageBox.Show("Вы добавили");
        //            break;
        //        case NotifyCollectionChangedAction.Remove:
        //            MessageBox.Show("Вы удалили");
        //            break;
        //        case NotifyCollectionChangedAction.Replace:
        //            MessageBox.Show("Вы заменили");
        //            break;
        //    }
        //}
        //private void _workClassesChanged(object sender, ListChangedEventArgs e)
        //{
        //    switch (e.ListChangedType)
        //    {
        //        case ListChangedType.ItemAdded:
        //            MessageBox.Show("Вы добавили элемент");
        //            break;
        //        case ListChangedType.ItemDeleted:
        //            MessageBox.Show("Вы удалили элемент");
        //            break;
        //        case ListChangedType.ItemChanged:
        //            MessageBox.Show("Вы изменили элемент");
        //            break;
        //    }
        //}
        private void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter);
            adapter.Update(_dataTable);
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateDB();
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ed_Homework.SelectedItems != null)
            {
                for (int i = 0; i < ed_Homework.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = ed_Homework.SelectedItems[i] as DataRowView;
                    if (datarowView != null)
                    {
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            UpdateDB();
        }
        private void PhonesGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            UpdateDB();
        }
    }
}