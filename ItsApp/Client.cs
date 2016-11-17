﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ItsAppServer;

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
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(DateTime.Now + " - You said: " + message.PadLeft(20));
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

                    bool IsJson = true;

                    try
                    {
                        var jsonobject = JsonConvert.DeserializeObject(message);
                        IsJson = true;
                    }
                    catch (Exception)
                    {
                        IsJson = false;
                    }
                    if (IsJson)
                    {
                        Message output = JsonConvert.DeserializeObject<Message>(message);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{output.TimeStamp} - {output.SentBy}: {output.Input}".PadLeft(20));
                    }else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(message.PadLeft(20));
                    }
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
