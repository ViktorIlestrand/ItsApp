using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAppServer
{
    public class Message
    {
        public int Recipient { get; set; }
        public DateTime TimeStamp { get; set; }
        public string SentBy { get; set; }
        public string Input { get; set; }

        public Message(string sentBy, string input, int recipient = 0)
        {
            SentBy = sentBy;
            Input = input;
            TimeStamp = DateTime.Now;
            Recipient = recipient;
        }
    }
}
