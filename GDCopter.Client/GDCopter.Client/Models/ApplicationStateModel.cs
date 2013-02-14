namespace GDCopter.Client.Models
{
    class ApplicationStateModel : ModelBase
    {
        private bool _isallSensorsData;

        private bool _isConnection;

        private bool _isTerminal;
        
        private bool _isCompassChart;

        private bool _orientationChart;

        private ApplicationState _applicationState;

        public bool IsOrientationChart
        {
            get { return _orientationChart; }
            set
            {
                _orientationChart = value; OnPropertyChanged("IsOrientationChart");
                if (value)
                {
                    ApplicationState = ApplicationState.OrientationChart;
                }
            }
        }

        public bool IsCompassChart
        {
            get { return _isCompassChart; }
            set
            {
                _isCompassChart = value; OnPropertyChanged("IsCompassChart");
                if (value)
                {
                    ApplicationState = ApplicationState.CompassChart;
                }
            }
        }

        public bool IsAllSensrosData
        {
            get { return _isallSensorsData; }
            set
            {
                _isallSensorsData = value;
                OnPropertyChanged("IsAllSensrosData");
                if (value)
                {
                    ApplicationState = ApplicationState.AllSensorsChart;
                }
            }
        }

        public bool IsTerminal
        {
            get { return _isTerminal; }
            set
            {
                _isTerminal = value;
                OnPropertyChanged("IsTerminal");
                if (value)
                {
                    ApplicationState = ApplicationState.Terminal;
                }
            }
        }

        public bool IsConnection
        {
            get { return _isConnection; }
            set
            {
                _isConnection = value;
                OnPropertyChanged("IsConnection");
                if (value)
                {
                    ApplicationState = ApplicationState.Connection;
                }
            }
        }

        public ApplicationState ApplicationState
        {
            get { return _applicationState; }
            set { _applicationState = value; OnPropertyChanged("ApplicationState"); }
        }
    }
}
