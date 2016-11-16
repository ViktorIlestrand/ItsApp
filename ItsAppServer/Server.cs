using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ItsAppServer
{
    public class Server
    {
        public List<ClientHandler> ConnectedUsers = new List<ClientHandler>();

        private TcpListener listener { get; set; }

        public void Run()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 9965);
                Console.WriteLine("Server is up and running!");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                listener.Start();
                while (true)
                {
                    TcpClient newTcpClient = listener.AcceptTcpClient();
                    ClientHandler newClient = new ClientHandler("Server Slayer" , newTcpClient, this);
                    ConnectedUsers.Add(newClient);
                    Thread client = new Thread(newClient.Run);

                    Console.WriteLine("New user added");

                    client.Start();
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if(listener != null)
                {
                    listener.Stop();
                }
            }     
                        

        }

       
    }
}
