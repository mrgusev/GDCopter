using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.Client.Models
{
    class OrientationModel : INotifyPropertyChanged
    {
        private double _yaw;

        private double _pitch;

        private double _roll;

        private double _altitude;

        public double Yaw
        {
            get { return _yaw; }
            set
            {
                _yaw = value;
                OnPropertyChanged("Yaw");
            }
        }

        public double Pitch
        {
            get { return _pitch; }
            set
            {
                _pitch = value;
                OnPropertyChanged("Pitch");
            }
        }

        public double Roll
        {
            get { return _roll; }
            set
            {
                _roll = value;
                OnPropertyChanged("Roll");
            }
        }

        public double Altitude
        {
            get { return _altitude; }
            set
            {
                _altitude = value;
                OnPropertyChanged("Altitude");
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
