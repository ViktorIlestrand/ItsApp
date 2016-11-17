using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAppServer
{
    public class Message
    {
        static int nextmsgId = 1000;
        public int Recipient { get; set; }
        public DateTime TimeStamp { get; set; }
        public int SentBy { get; set; }
        public string Input { get; set; }
        public int MessageId { get; set; }
        public string SentByName { get; set; }


        public Message(int sentBy, string input, int writingTo, string sentbyname)
        {
            SentByName = sentbyname;
            SentBy = sentBy;
            Input = input;
            TimeStamp = DateTime.Now;
            Recipient = writingTo;
            MessageId = nextmsgId;
            nextmsgId++;
        }
    }
}
