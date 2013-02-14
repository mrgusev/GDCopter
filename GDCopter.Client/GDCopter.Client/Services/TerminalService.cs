using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDCopter.Client.Models;
using Microsoft.Win32;

namespace GDCopter.Client.Services
{
    public class TerminalService :ServiceBase
    {

        public TerminalModel TerminalModel { get; private set; }

        public TerminalService(CommunicationModule communicationModule, TerminalModel terminalModel)
            :base(communicationModule,terminalModel)
        {
            TerminalModel = terminalModel;
            communicationModule.DataRecieved += CommunicationServiceDataRecieved;
            TerminalModel.PropertyChanged += TerminalModelPropertyChanged;
        }

        private void TerminalModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Message")
            {
                SendMessage(TerminalModel.Message);
            }
            
        }

        private void CommunicationServiceDataRecieved(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (IsRunning)
            {
                TerminalModel.RecievedMessages.Add(new KeyValuePair<string, DateTime>(CommunicationModule.LastMessage, DateTime.Now));
            }
        }

        public void SendMessage(string message)
        {
            if (IsRunning && CommunicationModule.ConnectionModel.IsRunning)
            {
                CommunicationModule.SendMessage(message);
                TerminalModel.SentMessages.Add(new KeyValuePair<string, DateTime>(message + "\n", DateTime.Now));
            }
        }

        public void SaveHistoryToFile()
        {
            string filePath = "";
            var result = TerminalModel.RecievedMessages.ToList();
            result.AddRange(TerminalModel.SentMessages);
            string[] history = result.OrderBy(p => p.Value).Select(s => s.Key).ToArray();
            File.WriteAllLines(filePath, history);
        }

        public override void Stop()
        {
            base.Stop();
            CommunicationModule.SendMessage(ControllerCommands.Stop);
        }
    }
}
