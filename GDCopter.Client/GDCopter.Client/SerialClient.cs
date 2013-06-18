using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GDCopter.Client
{
    public class DataStreamEventArgs : EventArgs
    {
        #region Fields
        private IEnumerable<float[]> _bytes;
        #endregion

        #region Constructors
        public DataStreamEventArgs(IEnumerable<float[]> bytes)
        {
            _bytes = bytes;
        }
        #endregion

        #region Properties
        public IEnumerable<float[]> Response
        {
            get { return _bytes; }
        }
        #endregion
    }

    public class SerialClient : IDisposable
    {
        #region Fields
        private string _port;
        private int _baudRate;
        private SerialPort _serialPort;
        private Thread serThread;
        private double _PacketsRate;
        private DateTime _lastReceive;
        /*The Critical Frequency of Communication to Avoid Any Lag*/
        private const int freqCriticalLimit = 20;
        #endregion

        #region Constructors
        public SerialClient(string port)
        {
            _port = port;
            _baudRate = 9600;
            _lastReceive = DateTime.MinValue;

            serThread = new Thread(new ThreadStart(SerialReceiving));
            serThread.Priority = ThreadPriority.Normal;
            serThread.Name = "SerialHandle" + serThread.ManagedThreadId;
        }
        public SerialClient(string Port, int baudRate)
            : this(Port)
        {
            _baudRate = baudRate;
        }
        #endregion

        #region Custom Events
        public event EventHandler<DataStreamEventArgs> OnReceiving;
        #endregion

        #region Properties
        public string Port
        {
            get { return _port; }
        }
        public int BaudRate
        {
            get { return _baudRate; }
        }
        public string ConnectionString
        {
            get
            {
                return String.Format("[Serial] Port: {0} | Baudrate: {1}",
                    _serialPort.PortName, _serialPort.BaudRate.ToString());
            }
        }
        #endregion

        #region Methods
        #region Port Control

        //creates and tries to open a port with the set characteristics
        //starts the thread
        //return false if failed, true if success
        public bool OpenConn()
        {
            try
            {
                if (_serialPort == null)
                    _serialPort = new SerialPort(_port, _baudRate, Parity.None);

                if (!_serialPort.IsOpen)
                {
                    _serialPort.ReadTimeout = -1;
                    _serialPort.WriteTimeout = -1;

                    _serialPort.Open();
                    _serialPort.DiscardInBuffer();

                    if (_serialPort.IsOpen)
                        serThread.Start(); /*Start The Communication Thread*/
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        //tries to open a port with the characteristics in parameters
        public bool OpenConn(string port, int baudRate)
        {
            _port = port;
            _baudRate = baudRate;

            return OpenConn();
        }

        //обрывает поток и закрывает порт
        public void CloseConn()
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                serThread.Abort();

                if (serThread.ThreadState == ThreadState.Aborted)
                    _serialPort.Close();
            }
        }

        //closes and opens port with the set characteristics
        public bool ResetConn()
        {
            CloseConn();
            return OpenConn();
        }

        #endregion
        #region Transmit/Receive

        //по сути - обертка над write
        public void Transmit(byte[] packet)
        {
            _serialPort.Write(packet, 0, packet.Length);
        }

        //считывает в массив в параметах байты из буффера, возвращает количество считанных байт
        //по сути - обертка
        public int Receive(byte[] bytes, int offset, int count)
        {
            int readBytes = 0;

            if (count > 0)
            {
                readBytes = _serialPort.Read(bytes, offset, count);
            }

            return readBytes;
        }
        #endregion

        #region IDisposable Methods

        //закрывает и освобождает порт
        public void Dispose()
        {
            CloseConn();

            if (_serialPort != null)
            {
                _serialPort.Dispose();
                _serialPort = null;
            }
        }
        #endregion
        #endregion

        #region Threading Loops
        private void SerialReceiving()
        {
            while (true)
            {
                int count = _serialPort.BytesToRead;

                /*Get Sleep Inteval*/
                TimeSpan tmpInterval = (DateTime.Now - _lastReceive);

                /*Form The Packet in The Buffer*/
                int packetsAmount = count / 64;
                byte[] buf = new byte[packetsAmount*64];
                int readBytes = Receive(buf, 0, packetsAmount*64);
                float[] floatsArray = new float[readBytes/4];
                Buffer.BlockCopy(buf, 0, floatsArray, 0, buf.Length);
                List<float[]> packets = new List<float[]>();
                for (int i = 0 , k = 0; i < packetsAmount; i++)
                {
                    packets.Add(new float[16]);
                    for (int j = 0; j < 17; j++, k++)
                    {
                        packets[i][j] = floatsArray[k];
                    }
                }
                if (readBytes > 0)
                {
                    OnSerialReceiving(packets);
                }

                #region Frequency Control
                _PacketsRate = ((_PacketsRate + readBytes) / 2);

                _lastReceive = DateTime.Now;

                if ((double)(readBytes + _serialPort.BytesToRead) / 2 <= _PacketsRate)
                {
                    if (tmpInterval.Milliseconds > 0)
                        Thread.Sleep(tmpInterval.Milliseconds > freqCriticalLimit ? freqCriticalLimit : tmpInterval.Milliseconds);

                    /*Testing Threading Model*/
                    //Diagnostics.Debug.Write(tmpInterval.Milliseconds.ToString());
                    //Diagnostics.Debug.Write(" - ");
                    //Diagnostics.Debug.Write(readBytes.ToString());
                    //Diagnostics.Debug.Write("\r\n");
                }
                #endregion
            }

        }
        #endregion

        #region Custom Events Invoke Functions
        private void OnSerialReceiving(IEnumerable<float[]> res)
        {
            if (OnReceiving != null)
            {
                OnReceiving(this, new DataStreamEventArgs(res));
            }
        }
        #endregion
    }
}
