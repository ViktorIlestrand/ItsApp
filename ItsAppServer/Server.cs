using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;

namespace ItsAppServer
{
    public class Server
    {
        public static List<ClientHandler> ConnectedUsers = new List<ClientHandler>();
        public TcpListener listener = new TcpListener(IPAddress.Any, 9965);
        public void Run()
        {

            try
            {
                              
                MessageListener msglistener = new MessageListener(this);
                Thread thread = new Thread(msglistener.Run);
                thread.Start();
                DatabaseTools dbtools = new DatabaseTools();
                dbtools.ClearDB();
                dbtools.Start();
                Console.WriteLine("Server is up and running!");
                while (true)
                {
                        listener.Start();
                        TcpClient newTcpClient = listener.AcceptTcpClient();
                        var newUser = new AddUser(newTcpClient, this);
                        Thread addUser = new Thread(newUser.Run);
                        addUser.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                               
            }     
                        

        }
       
        public void Broadcast(Message message)
        {
            DatabaseTools DBtools = new DatabaseTools();
            if (message.Recipient == 0)
            {

                string output = JsonConvert.SerializeObject(message);
                DBtools.AddMessage(message);
                foreach (var user in Server.ConnectedUsers)
                {

                    if (user.TcpClient.Connected && !user.Name.Equals(message.SentBy))
                    {
                        try
                        {
                            var Writer = new NetworkIO(user.TcpClient);
                            Writer.Write(output);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }
                }
            }else
            {
                string output = JsonConvert.SerializeObject(message);

                for (int i = 0; i < ConnectedUsers.Count(); i++)
                {
                    if(message.Recipient == ConnectedUsers[i].Id && ConnectedUsers[i].TcpClient.Connected)
                    {
                        var Writer = new NetworkIO(ConnectedUsers[i].TcpClient);
                        Writer.Write(output);
                        DBtools.AddMessage(message);
                        break;
                    }else
                    {

                    }
                }

                
            }

        }
        

    }
}
