using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ItsAppServer
{
    class MessageSender
    {
        public void Run(TcpClient client)
        {
            string message = "";
            NetworkStream n = client.GetStream();

            if (MessageQueue.Messages.Count != 0)
            {
                
                message = MessageQueue.Messages.Last();
                BinaryWriter writer = new BinaryWriter(n);
                writer.Write(message);
                writer.Flush();
            }
        }
    }
}
