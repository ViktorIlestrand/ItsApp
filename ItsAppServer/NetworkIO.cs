using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ItsAppServer
{
    public class NetworkIO
    {
        TcpClient tcp;

        public NetworkIO(TcpClient t)
        {
            tcp = t;
        }

        public void Write(string toWrite)
        {
            NetworkStream stream = tcp.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(toWrite);
            writer.Flush();
        }
        public string ReadString()
        {
            NetworkStream stream = this.tcp.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            return reader.ReadString();
        }

    }
}
