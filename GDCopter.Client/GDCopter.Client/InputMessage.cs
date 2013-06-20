using System;
using GDCopter.Client.Models;

namespace GDCopter.Client
{
    public class InputMessage
    {
        public InputMessage(float [] bytes)
        {
            var time = DateTime.Now;
            Orientation = new StatisticPoint(bytes[0] , bytes[1] , bytes[2], time);
            Gyro = new StatisticPoint(bytes[3], bytes[4], bytes[5], time);
            Accell = new StatisticPoint(bytes[6], bytes[7], bytes[8], time);
            Compass = new StatisticPoint(bytes[9], bytes[10], bytes[11], time);
            Rotor1 = new SimpleStatisticPoint(Math.Sqrt(bytes[12]), time);
            Rotor2 = new SimpleStatisticPoint(Math.Sqrt(bytes[13]), time);
            Rotor3 = new SimpleStatisticPoint(Math.Sqrt(bytes[14]), time);
            Rotor4 = new SimpleStatisticPoint(Math.Sqrt(bytes[15]), time);
            Pressure = new SimpleStatisticPoint(bytes[16], time);

        }
        public StatisticPoint Orientation { get; set; }
        public StatisticPoint Gyro { get; set; }
        public StatisticPoint Accell { get; set; }
        public StatisticPoint Compass { get; set; }
        public SimpleStatisticPoint Pressure { get; set; }

        public SimpleStatisticPoint Rotor1 { get; set; }
        public SimpleStatisticPoint Rotor2 { get; set; }
        public SimpleStatisticPoint Rotor3 { get; set; }
        public SimpleStatisticPoint Rotor4 { get; set; }
    }
}
