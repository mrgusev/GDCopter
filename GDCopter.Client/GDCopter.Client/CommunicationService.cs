using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace GDCopter.Client
{
    public class CommunicationService
    {
        private readonly SerialPort _serialPort = new SerialPort();

        private DateTime _previuosTime;

        private StatisticPoint _gyroValues = new StatisticPoint();

        private StatisticPoint _compassValues = new StatisticPoint();

        private StatisticPoint _accellValues = new StatisticPoint();

        Random _rand = new Random();

        public CommunicationService()
        {
            _serialPort.PortName = "COM3";
            _serialPort.BaudRate = 9600;

            _serialPort.DataReceived += _serialPort_DataReceived;

            _serialPort.Open();
            _previuosTime = DateTime.Now;
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ParseData(_serialPort.ReadLine());
            _previuosTime = DateTime.Now;


        }

        private void ParseData(string data)
        {
            DateTime time = DateTime.Now;
            string[] points = data.Split('#');
            if (points.Count() == 3)
            {
                _gyroValues = new StatisticPoint(double.Parse(points[0].Replace('.', ',')), double.Parse(points[1].Replace('.', ',')),
                        double.Parse(points[2].Replace('.', ',')), time);

            }
            //string[] points = data.Split(';');
            //if (points.Count() == 3)
            //{
            //    string[] gyroPoint = points[0].Split('#');
            //    string[] accellPoint = points[1].Split('#');
            //    string[] compassPoint = points[2].Split('#');
            //    if (gyroPoint.Count() == 3 && accellPoint.Count() == 3 && compassPoint.Count() == 3)
            //    {

            //        DateTime time = DateTime.Now;
            //        _gyroValues = new StatisticPoint(double.Parse(gyroPoint[0]), double.Parse(gyroPoint[1]),
            //            double.Parse(gyroPoint[2]), time);
            //        _accellValues = new StatisticPoint(double.Parse(accellPoint[0]), double.Parse(accellPoint[1]),
            //            double.Parse(accellPoint[2]), time);
            //        _compassValues = new StatisticPoint(double.Parse(compassPoint[0]), double.Parse(compassPoint[1]),
            //            double.Parse(compassPoint[2]), time);
            //    }
            //}
        }

        public StatisticPoint AccellValues
        {
            get
            {
                return _accellValues;
            }
        }
        public StatisticPoint GyroValues
        {
            get
            {
                return _gyroValues;
            }
        }
        public StatisticPoint CompassValues
        {
            get
            {
                return _compassValues;
            }
        }

    }
}
