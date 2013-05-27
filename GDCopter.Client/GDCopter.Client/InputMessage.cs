using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GDCopter.Client
{
    public class InputMessage
    {
        public InputMessage(float [] bytes)
        {
            var time = DateTime.Now;
            Orientation = new StatisticPoint(bytes[0], bytes[1], bytes[2], time);
            Gyro = new StatisticPoint(bytes[3], bytes[4], bytes[5], time);
            Accell = new StatisticPoint(bytes[6], bytes[7], bytes[8], time);
            Compass = new StatisticPoint(bytes[9], bytes[12], bytes[13], time);
        }
        public StatisticPoint Orientation { get; set; }
        public StatisticPoint Gyro { get; set; }
        public StatisticPoint Accell { get; set; }
        public StatisticPoint Compass { get; set; }
    }
}
