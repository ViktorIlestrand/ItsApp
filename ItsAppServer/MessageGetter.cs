using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ItsAppServer
{
    class MessageGetter
    {
        public string GetMessage()
        {
            string message = MessageQueue.Messages.Last();
            MessageQueue.Messages.Remove(MessageQueue.Messages.Last());
            return message;
        }
    }
}
