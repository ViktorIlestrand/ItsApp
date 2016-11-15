using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ItsAppServer
{
    class Server
    {
        public List<ClientHandler> ConnectedUsers { get; set; }

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
                    ClientHandler newClient = new ClientHandler("Benke", newTcpClient, this);
                    ConnectedUsers.Add(newClient);
                    Console.WriteLine("New user added");
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
