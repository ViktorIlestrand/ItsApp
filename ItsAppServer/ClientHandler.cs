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
        static int nextId = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public TcpClient TcpClient { get; set; }
        private Server Server { get; set; }
        public int WritingTo { get; set; }
        public ClientHandler(string name, TcpClient tcp, Server server)
        {
            Name = name;
            TcpClient = tcp;
            Server = server;
            Id = nextId;
            WritingTo = 0;
            nextId++;

        }

        public void Run()
        {
            try
            {
                string message = "";
                               

                while (!message.ToLower().Equals("quit")) //TODO: EVERYONE GETS THROWN OUT IF SOMEONE WRITES QUIT
                {
                    var Reader = new NetworkIO(this.TcpClient);
                    var Writer = new NetworkIO(this.TcpClient);
                    message = Reader.ReadString();

                    switch (message)
                    {
                        case "changename":
                            Writer.Write("Vilket namn vill du byta till?");
                            this.Name = Reader.ReadString();
                            break;
                        case "myname":
                            Writer.Write("Ditt nuvarande chattnamn är: " + this.Name);
                            break;
                        case "whoishere":
                            foreach (var user in Server.ConnectedUsers)
                            {
                                if (user != this)
                                {
                                    Writer.Write(user.Id + " " + user.Name);
                                }else
                                {

                                }
                            }
                            break;
                        case "w":
                            Writer.Write("0 Alla i chatrummet");
                            foreach (var user in Server.ConnectedUsers)
                            {
                                if (user != this)
                                {
                                    Writer.Write(user.Id + " " + user.Name);
                                }else
                                {

                                }
                            }
                            Writer.Write("Vem vill du viska till? Ange ID enligt listan ovan.");
                            string whisperTo = Reader.ReadString();
                            try
                            {
                                this.WritingTo = Convert.ToInt32(whisperTo);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Writer.Write("Något gick fel. Endast siffror är tillåtna.");
                            }
                                                                                    
                            break;
                        case "help":
                            Writer.Write("\nw: Ändra mottagare av dina meddelanden\nmyname: Visar ditt nuvarande chattnamn\nchangename: Ändra ditt chattnamn\nwhoishere: Listar alla chattdeltagare\n");
                            break;

                        default:
                            var tmpMsg = new Message(this.Name, message, this.WritingTo); 
                            MessageQueue.AddMessage(tmpMsg);
                            Console.WriteLine(message);
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
        
    }
}
