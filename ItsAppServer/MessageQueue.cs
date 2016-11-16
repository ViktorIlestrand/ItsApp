using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAppServer
{
    public static class MessageQueue
    {
        static List<Message> messages = new List<Message>();

        public static void AddMessage(Message messageToAdd)
        {
            messages.Add(messageToAdd);
        }

        public static int CheckQueue()
        {
            return messages.Count();
        }

        public static Message ReadAndRemove()
        {
            Message toBroadcast = messages.Last();
            MessageQueue.messages.Remove(MessageQueue.messages.Last());
            return toBroadcast;
        }
        //public static string GetMessage()
        //{
            // TODO 
        //}
    }
}
