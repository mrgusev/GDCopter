using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using GDCopter.Client.Models;

namespace GDCopter.Client.Services
{
    public class AllSensorsDataService : ServiceBase
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        public AllSensorsDataService(CommunicationModule communicationModule, AllSensorsDataModel dataModel)
            : base(communicationModule, dataModel)
        {
            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (IsRunning && CommunicationModule.LastMessage!=null)
            {
                var message = CommunicationModule.LastMessage;
                var dataModel = (AllSensorsDataModel)Model;
                dataModel.GyroValues.Add(message.Gyro);
                dataModel.AccelValues.Add(message.Accell);
                dataModel.CompassValues.Add(message.Compass);
                if (dataModel.GyroValues.Count > 50)
                    dataModel.GyroValues.RemoveAt(0);
                if (dataModel.AccelValues.Count > 50)
                    dataModel.AccelValues.RemoveAt(0);
                if (dataModel.CompassValues.Count > 50)
                    dataModel.CompassValues.RemoveAt(0);
                dataModel.Update();
            }
        }

        public override void Run()
        {
            base.Run();
            CommunicationModule.SendMessage(ControllerCommands.AllSensorsData);
            _timer.Start();
        }

        public override void Stop()
        {
            base.Stop();
            CommunicationModule.SendMessage(ControllerCommands.Stop);
            _timer.Stop();
        }

        private StatisticPoint[] ParseAllData(string message)
        {
            var now = DateTime.Now;
            var result = new []{new StatisticPoint(), new StatisticPoint(),new StatisticPoint()  };
            var sensors = message.Split(';');
            if (sensors.Count() == 3)
            {
                var gyro = sensors[0].Split('#');
                var accel = sensors[1].Split('#');
                var compass = sensors[2].Split('#');
                if (gyro.Count() == 3 && accel.Count() == 3 && compass.Count() == 3)
                {
                    result[0] = new StatisticPoint(ParseValue(gyro[0]), ParseValue(gyro[1]), ParseValue(gyro[2]), now);
                    result[1] = new StatisticPoint(ParseValue(accel[0]), ParseValue(accel[1]), ParseValue(accel[2]), now);
                    result[2] = new StatisticPoint(ParseValue(compass[0]), ParseValue(compass[1]), ParseValue(compass[2]),
                                                   now);
                }
            }
            return result;
        }
    }
}
