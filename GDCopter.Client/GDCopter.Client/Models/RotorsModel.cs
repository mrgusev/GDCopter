using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.Client.Models
{
    public class RotorsModel : ModelBase
    {
        private int _rotor1 ;
        private int _rotor2;
        private int _rotor3;
        private int _rotor4;
        private bool _isAllRotorsStopped;

        public int Rotor1
        {
            get { return _rotor1; }
            set
            {
                _rotor1 = value;
                OnPropertyChanged("Rotor1");
            }
        }

        public int Rotor2
        {
            get { return _rotor2; }
            set
            {
                _rotor2 = value;
                OnPropertyChanged("Rotor2");
            }
        }

        public int Rotor3
        {
            get { return _rotor3; }
            set
            {
                _rotor3 = value;
                OnPropertyChanged("Rotor3");
            }
        }

        public int Rotor4
        {
            get { return _rotor4; }
            set
            {
                _rotor4 = value;
                OnPropertyChanged("Rotor4");
            }
        }

        public bool IsAllRotorsStopped
        {
            get { return _isAllRotorsStopped; }
            set
            {
                _isAllRotorsStopped = value;
                OnPropertyChanged("IsAllRotorsStopped");
            }
        }
    }
}
