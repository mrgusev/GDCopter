using System;
using System.Linq;
using System.Windows.Threading;
using GDCopter.Client.Models;

namespace GDCopter.Client.Services
{
    public class OrientationService : ServiceBase
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
                var values = CommunicationModule.LastMessage;
                var model = (OrientationModel)Model;
                model.Pitch = values.Orientation.X;
                model.Roll = values.Orientation.Z;
                model.Yaw = values.Orientation.Y;
                model.OrientationValues.Add(values.Orientation);
                if (model.OrientationValues.Count > 50)
                    model.OrientationValues.RemoveAt(0);
            }
        }

        public override void Run()
        {
            base.Run();
            CommunicationModule.SendMessage(ControllerCommands.Orientation);
            _timer.Start();
        }

        public override void Stop()
        {
            base.Stop();
            CommunicationModule.SendMessage(ControllerCommands.Stop);
            _timer.Stop();
        }
    }
}
