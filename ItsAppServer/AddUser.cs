using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ItsAppServer
{
    class AddUser
    {
        public TcpClient Tcp { get; set; }
        public Server Server { get; set; }
        public AddUser(TcpClient t, Server s)
        {
            Tcp = t;
            Server = s;
        }

        public void Run()
        {
            DatabaseTools dbtool = new DatabaseTools();
            if (Tcp.Connected)
            {
                try
                {
                    NetworkStream x = Tcp.GetStream();
                    BinaryWriter writer = new BinaryWriter(x);
                    writer.Write("Ange ditt chattnamn!");
                    writer.Flush();
                    NetworkStream y = Tcp.GetStream();
                    BinaryReader reader = new BinaryReader(y);
                    string name = reader.ReadString();

                    ClientHandler newClient = new ClientHandler(name, Tcp, Server);
                    Server.ConnectedUsers.Add(newClient);
                    dbtool.AddChatter(newClient);
                    Thread client = new Thread(newClient.Run);

                    client.Start();
                    Console.WriteLine(newClient.Name + " just joined the server");


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}
