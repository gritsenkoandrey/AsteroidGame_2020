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
            }
        }
    }
}
