using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.TempClient
{
    class ClientService
    {
        public SerialPort SerialPort { get; private set; }
        public ClientService()
        {
            SerialPort = new SerialPort();
        }

        public bool OpenConnection()
        {
            if (!SerialPort.IsOpen)
            {
                SerialPort = new SerialPort();
                SerialPort.PortName = "COM4";
                SerialPort.BaudRate = 57600;
                SerialPort.DataReceived += DataReceived;
                try
                {
                    SerialPort.Open();
                }
                catch (Exception)
                {
                }
            }
            return SerialPort.IsOpen;
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte firstByte = (byte)SerialPort.ReadByte();
            if(firstByte == '*')
            {
                Console.Write(SerialPort.ReadLine());
            }
            else
            {
                byte[] bytes = new byte[52];
                SerialPort.Read(bytes, 0, 52);
                var bytesList = new List<byte> {firstByte};
                bytesList.AddRange(bytes);
                Console.WriteLine((new InputMessage()).Parse(bytesList.ToArray()));
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                if (SerialPort.IsOpen)
                    SerialPort.Write(message);
            }
            catch (Exception)
            {
            }
        }
    }
}
