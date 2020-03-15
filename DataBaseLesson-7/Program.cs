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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //SqlCommand command = new SqlCommand(createExpression, connection);
                //command.ExecuteNonQuery(); // выполнили sql запрос, если запрос один раз выполнен, то выскочит ошибка

                //var command = new SqlCommand(sqlExpression, connection);
                //command.ExecuteNonQuery();


            }
        }

        private const string  createExpression = @"CREATE TABLE[dbo].[People] (
                                    [Id] INT IDENTITY(1, 1) NOT NULL,
                                    [FIO] NVARCHAR(MAX) COLLATE Cyrillic_General_CI_AS NOT NULL,
                                    [Birthday] NVARCHAR(MAX) NULL,
                                    [Email]    NVARCHAR(100) NULL,
                                    [Phone]    NVARCHAR(MAX) NULL,
                                    CONSTRAINT[PK_dbo.People] PRIMARY KEY CLUSTERED([Id] ASC));"; // sql запрос

        private const string sqlExpression = @"INSERT INTO People (FIO, Birthday,Email,Phone) VALUES ( N'Иванов Иван Иванович', '18.10.2001', 'somebody@gmail.com', '89164444444' );
                                     INSERT INTO People(FIO, Birthday, Email, Phone) VALUES( N'Петров Петр Петрович', '15.01.2001', 'somebody@mail.com', '8916555555')";
    }
}