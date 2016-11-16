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
            Thread ListenerThread = new Thread(Listener);
            
            SenderThread.Start();
            SenderThread.Join();
            
            ListenerThread.Start();
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
                
                while (!message.ToLower().Equals("quit"))
                {
                    NetworkStream n = client.GetStream();
                    message = new BinaryReader(n).ReadString();
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
