using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.Client.Models
{
    public class ModelBase : INotifyPropertyChanged
    {
        private bool _isRunning;

        private string _status = "Run";

        public string StatusAction
        {
            get { return _status; }
            private set
            {
                _status = value;
                OnPropertyChanged("StatusAction");
            }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if (value != _isRunning)
                {
                    _isRunning = value;
                    StatusAction = value ? "Stop" : "Run";
                    OnPropertyChanged("IsRunning");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
