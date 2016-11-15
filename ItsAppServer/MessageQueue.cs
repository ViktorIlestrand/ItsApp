using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAppServer
{
    static class MessageQueue
    {
        static public List<string> Messages { get; set; }

        public static void AddMessage(string messageToAdd)
        {
            Messages.Add(messageToAdd);
        }
        //public static string GetMessage()
        //{
            // TODO 
        //}
    }
}
