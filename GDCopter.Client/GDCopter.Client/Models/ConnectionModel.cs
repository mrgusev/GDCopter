namespace GDCopter.Client.Models
{
    public class ConnectionModel : ModelBase
    {
        private string _port;

        private int _baudRate;

        public string Port
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged("Port");
            }
        }

        public int BaudRate
        {
            get { return _baudRate; }
            set
            {
                _baudRate = value;
                OnPropertyChanged("BaudRate");
            }
        }

 
    }
}
