using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataBaseLesson_7
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;" // адрес подключения к серверу
                                    + "Initial Catalog=Lesson-7;"  // указывает название БД на сервере, которая будет использоваться по умолчанию
                                    + "Integrated Security=True;"; // имя пользователя, если true, то логин и пароль не нужны
                                                                   //+"Connect Timeout=30;"
                                                                   //+"Encrypt=False;" // шифрование данных при передаче информации
                                                                   //+"TrustServerCertificate=False;"
                                                                   //+"ApplicationIntent=ReadWrite;"
                                                                   //+"MultiSubnetFailover=False"

            #region ExecuteNonQuery и ExecuteScalar
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();

            //    SqlCommand command = new SqlCommand(createExpression, connection);
            //    command.ExecuteNonQuery(); // выполнили sql запрос, если запрос один раз выполнен, то выскочит ошибка

            //    var command = new SqlCommand(sqlExpression, connection);
            //    command.ExecuteNonQuery();

            //    var command = new SqlCommand(createStoredProc, connection);
            //    command.ExecuteNonQuery();

            //    var command = new SqlCommand(sqlExpression1, connection);
            //    var vId = Convert.ToInt32(command.ExecuteScalar());
            //}
            #endregion

            #region ExecuteReader пример 1
            //string sqlExpression = "SELECT * FROM People";
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand(sqlExpression, connection);
            //    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            //    if (reader.HasRows)        // Если есть данные
            //    {
            //        while (reader.Read())  // Построчно считываем данные
            //        {
            //            var vId = Convert.ToInt32(reader.GetValue(0));
            //            var vFIO = reader.GetString(1);
            //            var vEmail = reader["Email"];
            //            var vPhone = reader.GetString(reader.GetOrdinal("Phone"));
            //        }
            //    }
            //    reader.Close();
            //}
            #endregion

            #region ExecuteReader пример 2
            //string sqlExpression = "[dbo].[sp_GetPeople]";
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand(sqlExpression, connection);
            //    // Указываем, что команда представляет хранимую процедуру
            //    command.CommandType = System.Data.CommandType.StoredProcedure;
            //    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            //    if (reader.HasRows)       // Если есть данные
            //    {
            //        while (reader.Read()) // Построчно считываем данные
            //        {
            //            var vId = Convert.ToInt32(reader.GetValue(0));
            //            var vFIO = reader.GetString(1);
            //            var vEmail = reader["Email"];
            //            var vPhone = reader.GetString(reader.GetOrdinal("Phone"));
            //        }
            //    }
            //    reader.Close();
            //}
            #endregion

            #region SqlParameter
            string sqlWhereExpression = "SELECT COUNT(*) FROM People where Birthday = @Birthday";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlWhereExpression, connection);
                SqlParameter param = new SqlParameter("@Birthday", SqlDbType.NVarChar, -1);
                param.Value = "18.10.2001";
                command.Parameters.Add(param);

                var vId = Convert.ToInt32(command.ExecuteScalar());
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlWhereExpression, connection);
                command.Parameters.AddWithValue("@Birthday", "18.10.2001");

                var vId = Convert.ToInt32(command.ExecuteScalar());
            }
            #endregion

        }

        //private const string  createExpression = @"CREATE TABLE[dbo].[People] (
        //                            [Id] INT IDENTITY(1, 1) NOT NULL,
        //                            [FIO] NVARCHAR(MAX) COLLATE Cyrillic_General_CI_AS NOT NULL,
        //                            [Birthday] NVARCHAR(MAX) NULL,
        //                            [Email]    NVARCHAR(100) NULL,
        //                            [Phone]    NVARCHAR(MAX) NULL,
        //                            CONSTRAINT[PK_dbo.People] PRIMARY KEY CLUSTERED([Id] ASC));";

        //private const string sqlExpression = @"INSERT INTO People (FIO, Birthday,Email,Phone) VALUES ( N'Иванов Иван Иванович', '18.10.2001', 'somebody@gmail.com', '89164444444' );
        //                             INSERT INTO People(FIO, Birthday, Email, Phone) VALUES( N'Петров Петр Петрович', '15.01.2001', 'somebody@mail.com', '8916555555')";

        //public static string createStoredProc = @"CREATE PROCEDURE [dbo].[sp_GetPeople] AS SELECT * FROM People;";

        //private const string sqlExpression1 = @"INSERT INTO People (FIO, Birthday,Email,Phone) output INSERTED.ID VALUES (N'Сидоров Сидор Сидорович', '16.10.2007', 'somebody@gmail.com', '89164444444' );";
    }
}