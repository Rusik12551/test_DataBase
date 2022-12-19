using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;


//for (int i = 0; i < 10; i++)
//{
//    Thread.Sleep(500);
//    Console.WriteLine(i);
//}
//Thread currentThread = Thread.CurrentThread;
namespace test_DataBase
{
    class DataBase
    {
        //Нужно изменить строку подключения (скопировать с обозревателя серверов (CTRL+ALT+S) "test.mdf")
        //Подключение к БД
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rusik\Desktop\test_DataBase\test_DataBase\test.mdf;Integrated Security=True");

       
        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }
        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        public SqlConnection getConnection()
        {
            return sqlConnection;
        }

    }
} 
