using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using GDCopter.Client.Models;

namespace GDCopter.Client
{
    public class CommunicationModule
    {
        public SerialClient SerialClient { get; private set; }

        public ConnectionModel ConnectionModel { get; private set; }

        private InputMessage _receivedMessage;

        public CommunicationModule(ConnectionModel connectionModel)
        {
            ConnectionModel = connectionModel;
            connectionModel.PropertyChanged += ConnectionModelPropertyChanged;
        }

        public bool OpenConnection()
        {
            SerialClient = new SerialClient(ConnectionModel.Port, ConnectionModel.BaudRate);
            SerialClient.OnReceiving += DataReceived;
            try
            {
                SerialClient.OpenConn();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to connect to port", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            return true;
        }

        public bool CloseConnection()
        {
            SendMessage(ControllerCommands.Stop);
            SerialClient.CloseConn();
            SerialClient.OnReceiving -= DataReceived;
            return false;
        }

        private void ConnectionModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsRunning")
            {
                ConnectionModel.IsRunning = !ConnectionModel.IsRunning ? CloseConnection() : OpenConnection();
            }
        }

        private void DataReceived(object sender, DataStreamEventArgs dataStreamEventArgs)
        {
            try
            {
                _receivedMessage = new InputMessage(dataStreamEventArgs.Response.First());
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
                //if (SerialPort.IsOpen)
                //    SerialPort.Write(message);
            }
            catch (Exception)
            {
                //MessageBox.Show("Unable to send a message to port" + SerialPort.PortName,
                //                   "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public InputMessage LastMessage
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
