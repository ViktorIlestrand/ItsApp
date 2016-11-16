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
    class MessageListener
    {
        public Server server { get; set; }
        public MessageListener(Server srvr)
        {
            server = srvr;
        }
        public void Run()
        {
            while (true)
            {
                if (MessageQueue.Messages.Count != 0)
                {
                    string toBroadcast = MessageQueue.Messages.Last();
                    MessageQueue.Messages.Remove(MessageQueue.Messages.Last());
                    server.Broadcast(toBroadcast);
                }
                Thread.Sleep(100);
            }
        }
        
    }
}
