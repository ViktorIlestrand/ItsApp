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

namespace ItsAppServer
{
    public class Server
    {
        public static List<ClientHandler> ConnectedUsers = new List<ClientHandler>();

        private TcpListener listener { get; set; }

        public void Run()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 9965);
                Console.WriteLine("Server is up and running!");
                listener.Start();

                MessageListener msglistener = new MessageListener(this);
                Thread thread = new Thread(msglistener.Run);
                thread.Start();

                while (true)
                {
                    TcpClient newTcpClient = listener.AcceptTcpClient();
                    ClientHandler newClient = new ClientHandler("Server Slayer" , newTcpClient, this);
                    ConnectedUsers.Add(newClient);
                    Thread client = new Thread(newClient.Run);

                    Console.WriteLine(newClient.Name + " just joined");

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
        public void Broadcast(Message message)
        {
            string output = JsonConvert.SerializeObject(message);

            foreach (var user in Server.ConnectedUsers)
            {
                
                if (user.TcpClient.Connected && !user.Name.Equals(message.SentBy))
                {
                    try
                    {
                        NetworkStream x = user.TcpClient.GetStream();
                        BinaryWriter writer = new BinaryWriter(x);
                        writer.Write(output);
                        writer.Flush();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                }
            }
        }


    }
}
