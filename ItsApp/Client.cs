using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ItsApp
{
    class Client
    {
        private TcpClient client;
        

        public void Start()
        {
            string ip = "192.168.25.167";
            client = new TcpClient(ip, 9965);

            Thread SenderThread = new Thread(Sender);
            Thread ListenerThread = new Thread(Listener);
            
            SenderThread.Start();
            ListenerThread.Start();

            SenderThread.Join();
            ListenerThread.Join();
               
        }

        public void Sender()
        {
            string message = "";

            try
            {
                while (!message.ToLower().Equals("quit"))
                {
                    NetworkStream n = client.GetStream();
                    message = Console.ReadLine();
                    BinaryWriter writer = new BinaryWriter(n);
                    writer.Write(message);
                    writer.Flush();
                }

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Listener()
        {
            string message = "";

            try
            {
                NetworkStream stream = client.GetStream();

                while (!message.ToLower().Equals("quit"))
                {
                    var br = new BinaryReader(stream);
                    message = br.ReadString();

                    Console.WriteLine(message);
                }

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
