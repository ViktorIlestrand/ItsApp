using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAppServer
{
    public class Message
    {
        public DateTime TimeStamp { get; set; }
        public string SentBy { get; set; }
        public string Input { get; set; }

        public Message(string sentBy, string input)
        {
            SentBy = sentBy;
            Input = input;
            TimeStamp = DateTime.Now;
        }
    }
}
