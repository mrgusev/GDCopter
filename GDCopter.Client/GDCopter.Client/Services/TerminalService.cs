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
    public class TerminalService
    {
        private readonly CommunicationService _communicationService;

        public TerminalModel TerminalModel { get; private set; }

        public TerminalService(CommunicationService communicationService, TerminalModel terminalModel)
        {
            _communicationService = communicationService;
            TerminalModel = terminalModel;
            _communicationService.DataRecieved += CommunicationServiceDataRecieved;
            TerminalModel.PropertyChanged += TerminalModel_PropertyChanged;
        }

        private void TerminalModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Message")
            {
                SendMessage(TerminalModel.Message);
            }
        }

        private void CommunicationServiceDataRecieved(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (TerminalModel.IsTransmitting)
            {
                TerminalModel.RecievedMessages.Add(new KeyValuePair<string, DateTime>(_communicationService.ReadLastMessage(), DateTime.Now));
            }
        }

        public void SendMessage(string message)
        {
            if (TerminalModel.IsTransmitting && _communicationService.ConnectionModel.IsOpen)
            {
                _communicationService.SendMessage(message);
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

    }
}
