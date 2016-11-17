using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ItsAppServer
{
    public class DatabaseTools
    {
        static string connectionString = "Data Source=Localhost;Initial Catalog=Candy;Integrated Security=True";
        SqlConnection myConnection = new SqlConnection(connectionString);

        public void AddMessage(Message msg)
        {
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandText = $"INSERT INTO Post (MessageId, Msg, DateTime) VALUES ('{msg.MessageId}','{msg.Input}', '{msg.TimeStamp}')";
                myCommand.Connection = myConnection;
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = $"INSERT INTO ChatterMessage (Msg_Id, R_Id, S_Id) VALUES ('{msg.MessageId}', '{msg.Recipient}', '{msg.SentBy}')";
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
        public void AddChatter(ClientHandler ch)
        {
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandText = $"INSERT INTO Chatter (Id, Name) VALUES ('{ch.Id}', '{ch.Name}')";
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

        public void Start()
        {
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandText = $"INSERT INTO Chatter (Id, Name) VALUES ('0', 'Alla')";
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

        public void ClearDB()
        {
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = $"DELETE FROM ChatterMessage";
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = $"DELETE FROM Chatter";
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = $"DELETE FROM Post";
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
