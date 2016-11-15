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
    class ClientHandler
    {
        static int nextId;
        public int Id { get; set; }
        public string Name { get; set; }
        public TcpClient TcpClient { get; set; }
        public Server Server { get; set; }
        public ClientHandler(string name, TcpClient tcp, Server server)
        {
            Name = name;
            TcpClient = tcp;
            Server = server;
            Id = nextId;
            nextId++;

            Thread client = new Thread(Run);
            client.Start();
        }

        public void Run()
        {
            string message = "";

            while(message.ToLower() != "quit") //TODO: EVERYONE GETS THROWN OUT IF SOMEONE WRITES QUIT
            {
                NetworkStream stream = TcpClient.GetStream();
                message = new BinaryReader(stream).ReadString();
                MessageQueue.Messages.Add(message);

            }

            TcpClient.Close();

        }
    }
}
