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
        readonly string connectionString;
        SqlDataAdapter adapter;
        DataTable _dataTable;

        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ed_Homework.RowEditEnding += PhonesGrid_RowEditEnding;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM dataGrid";
            _dataTable = new DataTable();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                // установка команды на добавление для вызова хранимой процедуры
                adapter.InsertCommand = new SqlCommand("procedure", connection);
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
        }
        private void UpdateDB()
        {
            var comandbuilder = new SqlCommandBuilder(adapter);
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