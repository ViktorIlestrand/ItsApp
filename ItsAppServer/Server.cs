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
using System.Collections;

namespace ItsAppServer
{
    public class Server
    {
        public static List<ClientHandler> ConnectedUsers = new List<ClientHandler>();
        public static List<NewUserListener> availableJoinListeners = new List<NewUserListener>();
               
        public void Run()
        {
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    string name = "listener" + i;
                    NewUserListener tmp = new NewUserListener(name, this);
                    availableJoinListeners.Add(tmp);
                }

                //var activeListener = availableJoinListeners.First();
                //listener = activeListener.listener;
                //listener.Start();
                
                MessageListener msglistener = new MessageListener(this);
                Thread thread = new Thread(msglistener.Run);
                thread.Start();

                Console.WriteLine("Server is up and running!");
                while (true)
                {
                    if (availableJoinListeners.Count != 0)
                    {
                        var activeListener = availableJoinListeners.First();
                        availableJoinListeners.Remove(availableJoinListeners.First());
                        activeListener.listener.Start();
                        TcpClient newTcpClient = activeListener.listener.AcceptTcpClient();
                        activeListener.newTcpClient = newTcpClient;
                        Thread joinerThread = new Thread(activeListener.Run);
                        joinerThread.Start();
                        
                        //    if (newTcpClient.Connected)
                        //    {
                        //        try
                        //        {
                        //            NetworkStream x = newTcpClient.GetStream();
                        //            BinaryWriter writer = new BinaryWriter(x);
                        //            writer.Write("Ange ditt chattnamn!");
                        //            writer.Flush();
                        //            NetworkStream y = newTcpClient.GetStream();
                        //            BinaryReader reader = new BinaryReader(y);
                        //            name = reader.ReadString();

                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            Console.WriteLine(ex.Message);
                        //        }

                        //    }

                        //    ClientHandler newClient = new ClientHandler(name, newTcpClient, this);
                        //    ConnectedUsers.Add(newClient);
                        //    Thread client = new Thread(newClient.Run);

                        //    Console.WriteLine(newClient.Name + " just joined");

                        //    client.Start();
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                                
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
