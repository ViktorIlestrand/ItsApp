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
    public class ClientHandler
    {
        static int nextId;
        public int Id { get; set; }
        public string Name { get; set; }
        public TcpClient TcpClient { get; set; }
        private Server Server { get; set; }
        public ClientHandler(string name, TcpClient tcp, Server server)
        {
            Name = name;
            TcpClient = tcp;
            Server = server;
            Id = nextId;
            nextId++;

        }

        public void Run()
        {
            try
            {
                string message = "";

                while (!message.ToLower().Equals("quit")) //TODO: EVERYONE GETS THROWN OUT IF SOMEONE WRITES QUIT
                {
                    NetworkStream stream = TcpClient.GetStream();
                    var br = new BinaryReader(stream);
                    message = br.ReadString();

                    switch (message)
                    {
                        case "changename":
                            this.Name = "newName";
                            break;
                        case "myname":
                            Console.WriteLine(this.Name);
                            break;
                        default:
                            MessageQueue.AddMessage(message);
                            Console.WriteLine(message);
                            Broadcast(TcpClient);
                            break;
                    }

                }

                TcpClient.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public void Broadcast(TcpClient client)
        {
            var getter = new MessageGetter();
            string message = getter.GetMessage();

            foreach (var user in Server.ConnectedUsers)
            {
                NetworkStream x = user.TcpClient.GetStream();
                BinaryWriter writer = new BinaryWriter(x);
                writer.Write(message);
                writer.Flush();
            }
        }
    }
}
