using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.Client.Models
{
    class RotorTestModel : INotifyPropertyChanged
    {
        private int _rotor1Thrust;

        private int _rotor2Thrust;

        private int _rotor3Thrust;

        private int _rotor4Thrust;

        public int Rotor1Thrust
        {
            get { return _rotor1Thrust; }
            set
            {
                _rotor1Thrust = value;
                OnPropertyChanged("Rotor1Thrust");
            }
        }
        public int Rotor2Thrust
        {
            get { return _rotor2Thrust; }
            set
            {
                _rotor2Thrust = value;
                OnPropertyChanged("Rotor2Thrust");
            }
        }
        public int Rotor3Thrust
        {
            get { return _rotor3Thrust; }
            set
            {
                _rotor3Thrust = value;
                OnPropertyChanged("Rotor3Thrust");
            }
        }
        public int Rotor4Thrust
        {
            get { return _rotor4Thrust; }
            set
            {
                _rotor4Thrust = value;
                OnPropertyChanged("Rotor4Thrust");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
