using System;
using GDCopter.Client.Models;

namespace GDCopter.Client
{
    public class InputMessage
    {
        public InputMessage(float [] bytes)
        {
            var time = DateTime.Now;
            Orientation = new StatisticPoint(bytes[0] * 57.295, bytes[1] * 57.295, bytes[2] * 57.295, time);
            Gyro = new StatisticPoint(bytes[3], bytes[4], bytes[5], time);
            Accell = new StatisticPoint(bytes[6], bytes[7], bytes[8], time);
            Compass = new StatisticPoint(bytes[9], bytes[10], bytes[11], time);
        }
        public StatisticPoint Orientation { get; set; }
        public StatisticPoint Gyro { get; set; }
        public StatisticPoint Accell { get; set; }
        public StatisticPoint Compass { get; set; }

        public SimpleStatisticPoint Rotor1 { get; set; }
        public SimpleStatisticPoint Rotor2 { get; set; }
        public SimpleStatisticPoint Rotor3 { get; set; }
        public SimpleStatisticPoint Rotor4 { get; set; }
    }
}
