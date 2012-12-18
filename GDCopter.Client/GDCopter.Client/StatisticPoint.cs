using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.Client
{
    public class StatisticPoint
    {
        private double _x;
        private double _y;
        private double _z;
        private DateTime _time;
        public StatisticPoint()
        {
            _x = 0;
            _y = 0;
            _z = 0;
            _time = DateTime.Now;
        }
        public StatisticPoint(double x, double y, double z, DateTime time)
        {
            _x = x;
            _y = y;
            _z = z;
            _time = time;
        }

        public double X { get { return _x; } }
        public double Y { get { return _y; } }
        public double Z { get { return _z; } }
        public DateTime Time { get { return _time; } }
    }
}
