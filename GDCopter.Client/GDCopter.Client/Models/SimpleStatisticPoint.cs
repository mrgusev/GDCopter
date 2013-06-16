using System;

namespace GDCopter.Client.Models
{
    public class SimpleStatisticPoint
    {
        private double _value;
        private DateTime _time;
        public SimpleStatisticPoint()
        {
            _value = 0;
            _time = DateTime.Now;
        }
        public SimpleStatisticPoint(double value, DateTime time)
        {
            _value = value;
            _time = time;
        }

        public double Value { get { return _value; } }
        public DateTime Time { get { return _time; } }
    }
}
