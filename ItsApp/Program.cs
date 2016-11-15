using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ItsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            Thread thread = new Thread(client.Start);

            thread.Start();
            thread.Join();

        }

    }
}
