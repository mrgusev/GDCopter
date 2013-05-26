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
            SerialClient serialClient = new SerialClient("COM3", 57600);
            serialClient.OnReceiving += serialClient_OnReceiving;
            serialClient.OpenConn();
            
        }

        static void serialClient_OnReceiving(object sender, DataStreamEventArgs e)
        {
            int packetsAmount = e.Response.GetLength(0);
            for (int i = 0; i < packetsAmount; i++)
            {
                Console.WriteLine("Pitch = {0}; Roll = {1}; Yaw = {2}", e.Response[i, 0] * 57.296, e.Response[i, 1] * 57.296, e.Response[i, 2] * 57.296);
            }
        }
    }
}
