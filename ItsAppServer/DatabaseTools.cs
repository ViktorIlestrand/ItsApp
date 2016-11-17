using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ItsAppServer
{
    class DatabaseTools
    {
        static string connectionString = "";
        SqlConnection myConnection = new SqlConnection(connectionString);

        public void AddToDB(Message msg)
        {
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandText = $"INSERT INTO Post (Msg, DateTime) VALUES ('{msg.Input}', '{msg.TimeStamp}')";
                myCommand.Connection = myConnection;
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}
