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
            Console.WriteLine("Yaw={0}\tPitch={1}\tRoll={2}", e.Response[0], e.Response[1], e.Response[2]);
        }
    }
}
