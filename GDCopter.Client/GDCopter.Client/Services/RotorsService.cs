using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using GDCopter.Client.Models;

namespace GDCopter.Client.Services
{
    public class RotorsService :ServiceBase
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        public RotorsService(CommunicationModule communicationModule, RotorsModel model) 
            : base(communicationModule, model)
        {
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += TimerTick;
        }


        private void TimerTick(object sender, EventArgs e)
        {
            SendValues();
        }

        private void SendValues()
        {
            var rotorsModel = (RotorsModel) Model;
            string message = String.Format("@{0}#{1}#{2}#{3}#", rotorsModel.Rotor1, rotorsModel.Rotor2, rotorsModel.Rotor3, rotorsModel.Rotor4);
           // CommunicationModule.SendMessage(message);
        }

        private void  StopAllRotors()
        {
            var rotorsModel = (RotorsModel)Model;
            rotorsModel.Rotor1 = rotorsModel.Rotor2 = rotorsModel.Rotor3 = rotorsModel.Rotor4 = 100;
          //  CommunicationModule.SendMessage("*stop");
        }

        public override void Run()
        {
            base.Run();
            var rotorsModel = (RotorsModel)Model;
          //  CommunicationModule.SendMessage("*directvalues");
            rotorsModel.Rotor1 = rotorsModel.Rotor2 = rotorsModel.Rotor3 = rotorsModel.Rotor4 = 100;
            Thread.Sleep(10);
            _timer.Start();
        }

        public override void Stop()
        {
            _timer.Stop();
            StopAllRotors();
            base.Stop();
        }
    }
}
