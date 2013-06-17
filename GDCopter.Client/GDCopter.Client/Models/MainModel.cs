using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDCopter.Client.Services;

namespace GDCopter.Client.Models
{
    class MainModel : ModelBase
    {
        public MainModel()
        {
            Services = new List<ServiceBase>();
            ConnectionModel = new ConnectionModel { BaudRate = 57600, Port = "COM3" };
            CompassDataModel = new CompassDataModel();
            OrientationModel = new OrientationModel();
            ControllerModel = new ControllerModel();
            AllSensorsDataModel = new AllSensorsDataModel();
            RotorsModel = new RotorsModel();
            TerminalModel = new TerminalModel();
            ApplicationStatusModel = new ApplicationStateModel { IsTerminal = true };
            ApplicationStatusModel.PropertyChanged += ApplicationStatusModelPropertyChanged;
            ConnectionModel.PropertyChanged += ConnectionModelPropertyChanged;
        }

        public List<ServiceBase> Services { get; private set; }

        public ConnectionModel ConnectionModel { get; private set; }

        public OrientationModel OrientationModel { get; private set; }

        public AllSensorsDataModel AllSensorsDataModel { get; private set; }

        public CompassDataModel CompassDataModel { get; private set; }

        public ApplicationStateModel ApplicationStatusModel { get; set; }

        public TerminalModel TerminalModel { get; private set; }

        public RotorsModel RotorsModel { get; private set; }

        public ControllerModel ControllerModel { get; private set; }

        private void ConnectionModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsRunning")
            {
                if (!ConnectionModel.IsRunning)
                {
                    Services.ForEach(s => s.Stop());
                }
            }
        }

        private void ApplicationStatusModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Services.ForEach(p => p.Stop());

            //switch (ApplicationStatusModel.ApplicationState)
            //{
            //    case ApplicationState.Terminal:
            //        break;
            //    case ApplicationState.AllSensorsChart:
            //        break;
            //    case ApplicationState.CompassChart:
            //        break;
            //    case ApplicationState.OrientationChart:
            //        break;
            //    case ApplicationState.Connection:
            //        break;
            //    case ApplicationState.Rototrs:
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //}
        }
    }
}
