using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.Client.Models
{
    public class ControllerModel : ModelBase
    {
        public List<SimpleStatisticPoint> Rotor1 { get; private set; }

        public List<SimpleStatisticPoint> Rotor2 { get; private set; }

        public List<SimpleStatisticPoint> Rotor3 { get; private set; }

        public List<SimpleStatisticPoint> Rotor4 { get; private set; }

        public List<SimpleStatisticPoint> Throttle { get; private set; }

        public double CurrentThrottle { get; set; }

        public ControllerModel()
        {
            Rotor1 = new List<SimpleStatisticPoint>();
            Rotor2 = new List<SimpleStatisticPoint>();
            Rotor3 = new List<SimpleStatisticPoint>();
            Rotor4 = new List<SimpleStatisticPoint>();
            Throttle = new List<SimpleStatisticPoint>();
            CurrentThrottle = 0;
        }

        public void Update()
        {
            OnPropertyChanged("Data");
        }
    }
}
