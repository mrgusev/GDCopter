using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.Client.Models
{
    public class TerminalModel : ModelBase
    {
        private string _receiveText;
        private string _sentText;
        private string _message;

        public ObservableCollection<KeyValuePair<string, DateTime>> RecievedMessages { get; set; }

        public ObservableCollection<KeyValuePair<string, DateTime>> SentMessages { get; set; }

        public string ReceiveText
        {
            get { return _receiveText; }
            set
            {
                _receiveText = value;
                OnPropertyChanged("ReceiveText");
            }
        }

        public string SentText
        {
            get { return _sentText; }
            set
            {
                _sentText = value;
                OnPropertyChanged("SentText");
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        public TerminalModel()
        {
            RecievedMessages = new ObservableCollection<KeyValuePair<string, DateTime>>();
            SentMessages = new ObservableCollection<KeyValuePair<string, DateTime>>();
            RecievedMessages.CollectionChanged += RecievedCollectionChanged;
            SentMessages.CollectionChanged += SentCollectionChanged;
        }

        private void SentCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SentText += ((KeyValuePair<string, DateTime>)e.NewItems[0]).Key;
        }

        private void RecievedCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            string temp = "";
            foreach (var item in RecievedMessages.Skip(RecievedMessages.Count-100))
            {
                temp += item.Key;
            }
            ReceiveText = temp;

        }

    }
}
