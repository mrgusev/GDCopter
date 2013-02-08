using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Windows;
using GDCopter.Client.Models;

namespace GDCopter.Client.Services
{
    public class CommunicationService
    {
        public SerialPort SerialPort { get; private set; }

        public ConnectionModel ConnectionModel { get; private set; }

        private string _receivedMessage;

        public CommunicationService(ConnectionModel connectionModel)
        {
            ConnectionModel = connectionModel;
            SerialPort = new SerialPort();
            connectionModel.PropertyChanged += connectionModelPropertyChanged;
            SerialPort.DataReceived += DataReceived;
        }

        public bool OpenConnection()
        {
            if (!SerialPort.IsOpen)
            {
                SerialPort.PortName = ConnectionModel.Port;
                SerialPort.BaudRate = ConnectionModel.BaudRate;
                try
                {
                    SerialPort.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to connect to port" + SerialPort.PortName,
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            var status = SerialPort.IsOpen;
            return status;
        }

        public void CloseConnection()
        {
            SerialPort.Close();
        }

        private void connectionModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsOpen")
            {
                if(!ConnectionModel.IsOpen)
                {
                    CloseConnection();
                }
                else
                {
                    OpenConnection();
                }
            }
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ConnectionModel.IsOpen = SerialPort.IsOpen;
            _receivedMessage = SerialPort.ReadLine();
            OnDataRecieved("Message");
        }

        public void SendMessage(string message)
        {
            ConnectionModel.IsOpen = SerialPort.IsOpen;
            SerialPort.Write(message);
        }

        public string ReadLastMessage()
        {
            return _receivedMessage;
        }

        //private void ParseData(string data)
        //{
        //DateTime time = DateTime.Now;
        //string[] points = data.Split('#');
        //if (points.Count() == 3)
        //{
        //    _gyroValues = new StatisticPoint(double.Parse(points[0].Replace('.', ',')), double.Parse(points[1].Replace('.', ',')),
        //            double.Parse(points[2].Replace('.', ',')), time);

        //}
        //string[] points = data.Split(';');
        //if (points.Count() == 3)
        //{
        //    string[] gyroPoint = points[0].Split('#');
        //    string[] accellPoint = points[1].Split('#');
        //    string[] compassPoint = points[2].Split('#');
        //    if (gyroPoint.Count() == 3 && accellPoint.Count() == 3 && compassPoint.Count() == 3)
        //    {

        //        DateTime time = DateTime.Now;
        //        _gyroValues = new StatisticPoint(double.Parse(gyroPoint[0]), double.Parse(gyroPoint[1]),
        //            double.Parse(gyroPoint[2]), time);
        //        _accellValues = new StatisticPoint(double.Parse(accellPoint[0]), double.Parse(accellPoint[1]),
        //            double.Parse(accellPoint[2]), time);
        //        _compassValues = new StatisticPoint(double.Parse(compassPoint[0]), double.Parse(compassPoint[1]),
        //            double.Parse(compassPoint[2]), time);
        //    }
        //}
        // }


        public event PropertyChangedEventHandler DataRecieved;

        protected virtual void OnDataRecieved(string propertyName)
        {
            PropertyChangedEventHandler handler = DataRecieved;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
