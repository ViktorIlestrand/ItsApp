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
            Console.WriteLine("Skriv in din IP-adress, Benke");
            string input = Console.ReadLine();
            client = new TcpClient(input, 9965);

            Thread SenderThread = new Thread(Sender);

            SenderThread.Start();
            SenderThread.Join();
            
               
        }

        public void Sender()
        {
            string message = "";

            try
            {
                NetworkStream n = client.GetStream();

                while (!message.ToLower().Equals("quit"))
                {
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
    }
}
