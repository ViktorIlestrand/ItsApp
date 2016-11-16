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
    public class NewUserListener
    {
        public string Name { get; set; }
        public TcpListener listener { get; set; }

        public TcpClient newTcpClient { get; set; }
        public Server server { get; set; }

        public NewUserListener(string name, Server servr)
        {
            Name = name;
            listener = new TcpListener(IPAddress.Any, 9965);
            server = servr;
            newTcpClient = null;
        }
        public void Run()
        {
            string name = "Server Slayer";
            

            if (newTcpClient.Connected)
            {
                try
                {
                    NetworkStream x = newTcpClient.GetStream();
                    BinaryWriter writer = new BinaryWriter(x);
                    writer.Write("Ange ditt chattnamn!");
                    writer.Flush();
                    NetworkStream y = newTcpClient.GetStream();
                    BinaryReader reader = new BinaryReader(y);
                    name = reader.ReadString();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            ClientHandler newClient = new ClientHandler(name, newTcpClient, server);
            Server.ConnectedUsers.Add(newClient);
            Thread client = new Thread(newClient.Run);

            Console.WriteLine(newClient.Name + " just joined");

            client.Start();
            Server.availableJoinListeners.Add(this);
        }

    }
}
