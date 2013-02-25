using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Windows;
using GDCopter.Client.Models;

namespace GDCopter.Client
{
    public class CommunicationModule
    {
        public SerialPort SerialPort { get; private set; }

        public ConnectionModel ConnectionModel { get; private set; }

        private string _receivedMessage;

        public CommunicationModule(ConnectionModel connectionModel)
        {
            ConnectionModel = connectionModel;
            SerialPort = new SerialPort();
            connectionModel.PropertyChanged += ConnectionModelPropertyChanged;
        }

        public bool OpenConnection()
        {
            if (!SerialPort.IsOpen)
            {
                SerialPort = new SerialPort();
                
                SerialPort.PortName = ConnectionModel.Port;
                SerialPort.BaudRate = ConnectionModel.BaudRate;
                SerialPort.DataReceived += DataReceived;
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

        public bool CloseConnection()
        {
            SendMessage(ControllerCommands.Stop);
            SerialPort.Close();
            SerialPort.DataReceived -= DataReceived;
            return SerialPort.IsOpen;
        }

        private void ConnectionModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsRunning")
            {
                if(!ConnectionModel.IsRunning)
                {
                    ConnectionModel.IsRunning = CloseConnection();
                }
                else
                {
                    ConnectionModel.IsRunning = OpenConnection();
                }
            }
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                _receivedMessage = SerialPort.ReadLine();
                OnDataRecieved("Message");
            }
            catch (Exception)
            {
                
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
                MessageBox.Show("Unable to send a message to port" + SerialPort.PortName,
                                   "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string LastMessage
        {
            get
            {
                return _receivedMessage;
            }
        }

        public event PropertyChangedEventHandler DataRecieved;

        protected virtual void OnDataRecieved(string propertyName)
        {
            PropertyChangedEventHandler handler = DataRecieved;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
