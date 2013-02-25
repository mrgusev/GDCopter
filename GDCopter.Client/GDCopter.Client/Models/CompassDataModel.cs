using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GDCopter.Client.Models
{
    public class CompassDataModel : ModelBase
    {
        private bool _isXY = true;
        private bool _isXz;
        private bool _isYz;

        public List<Point> Points { get; private set; }
        public bool IsXY
        {
            get { return _isXY; }
            set
            {
                _isXY = value;
                OnPropertyChanged("IsXY");
            }
        }

        public bool IsXZ
        {
            get { return _isXz; }
            set
            {
                _isXz = value;
                OnPropertyChanged("IsXZ");
            }
        }

        public bool IsYZ
        {
            get { return _isYz; }
            set
            {
                _isYz = value;
                OnPropertyChanged("IsYZ");
            }
        }

        public CompassDataModel()
        {
            Points = new List<Point>();
        }

        public void Update()
        {
            OnPropertyChanged("Data");
        }
    }
}
