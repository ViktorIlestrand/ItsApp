using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAppServer
{
    public static class MessageQueue
    {
        static public List<string> Messages { get; set; }

        public static void AddMessage(string messageToAdd)
        {
            Messages.Add(messageToAdd);
        }

        public static void PublishMessage()
        {

        }
        //public static string GetMessage()
        //{
            // TODO 
        //}
    }
}
