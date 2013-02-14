using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using GDCopter.Client.Models;

namespace GDCopter.Client.Services
{
    class OrientationService : ServiceBase
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        public OrientationService(CommunicationModule communicationModule, OrientationModel model)
            : base(communicationModule, model)
        {
            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (IsRunning && CommunicationModule.LastMessage!=null)
            {
                var values = ParseAllData(CommunicationModule.LastMessage);
                var model = (OrientationModel)Model;
                model.Pitch = values.X;
                model.Roll = values.Z;
                model.Yaw = values.Y;
                model.OrientationValues.Add(values);
                if (model.OrientationValues.Count > 50)
                    model.OrientationValues.RemoveAt(0);
            }
        }

        private StatisticPoint ParseAllData(string message)
        {
            var now = DateTime.Now;
            var result = new StatisticPoint();
            var values = message.Split('#');
            if (values.Count() == 3)
            {
                result = new StatisticPoint(ParseValue(values[0]) * 57.295, ParseValue(values[1]) * 57.295, ParseValue(values[2]) * 57.295, now);
            }
            return result;
        }

        public override void Run()
        {
            base.Run();
            CommunicationModule.SendMessage(ControllerCommands.Orientation);
            _timer.Start();
        }

        public void Stop()
        {
            base.Stop();
            CommunicationModule.SendMessage(ControllerCommands.Stop);
            _timer.Stop();
        }
    }
}
