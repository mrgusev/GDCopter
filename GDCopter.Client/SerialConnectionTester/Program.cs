using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SerialConnectionTester
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialClient serialClient = new SerialClient("COM4", 9600);
            serialClient.OnReceiving += serialClient_OnReceiving;
            serialClient.OpenConn();
        }

        static void serialClient_OnReceiving(object sender, DataStreamEventArgs e)
        {
            for (int i = 0; i < e.Response.Length; i++)
            {
                Console.WriteLine(e.Response[i].ToString());
            }
        }
    }
}
