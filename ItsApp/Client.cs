using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ItsApp
{
    class Client
    {
        private TcpClient client;

        public void Start()
        {
            Console.WriteLine("Skriv in din IP-adress, Benke");
            string input = Console.ReadLine();
            client = new TcpClient(input, 9965);
            Console.WriteLine("Ange ditt chatnamn: ");
            


        }
    }
}
