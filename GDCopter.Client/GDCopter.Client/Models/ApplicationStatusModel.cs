using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.Client.Models
{
    class ApplicationStatusModel : ModelBase
    {
        private bool _isTest;

        private bool _isControl;

        private bool _isProperties;

        private bool _isFlightData;

        private bool _isConnection;

        private bool _isTerminal;

        public bool IsTest
        {
            get { return _isTest; }
            set
            {
                _isTest = value;
                OnPropertyChanged("IsTest");
            }
        }

        public bool IsControl
        {
            get { return _isControl; }
            set
            {
                _isControl = value;
                OnPropertyChanged("IsControl");
            }
        }

        public bool IsProperties
        {
            get { return _isProperties; }
            set
            {
                _isProperties = value;
                OnPropertyChanged("IsProperties");
            }
        }

        public bool IsFlightData
        {
            get { return _isFlightData; }
            set
            {
                _isFlightData = value;
                OnPropertyChanged("IsFlightData");
            }
        }

        public bool IsTerminal
        {
            get { return _isTerminal; }
            set
            {
                _isTerminal = value;
                OnPropertyChanged("IsTerminal");
            }
        }

        public bool IsConnection
        {
            get { return _isConnection; }
            set
            {
                _isConnection = value;
                OnPropertyChanged("IsConnection");
            }
        }
    }
}
