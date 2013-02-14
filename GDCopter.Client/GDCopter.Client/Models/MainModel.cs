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
        private readonly List<ServiceBase> _services; 
        public MainModel()
        {
            ConnectionModel = new ConnectionModel{BaudRate = 115200, Port = "COM3"};
            OrientationModel = new OrientationModel();
            AllSensorsDataModel = new AllSensorsDataModel();
            TerminalModel = new TerminalModel();
            ApplicationStatusModel = new ApplicationStateModel{IsTerminal = true};
            ApplicationStatusModel.PropertyChanged += ApplicationStatusModelPropertyChanged;
            CommunicationModule = new CommunicationModule(ConnectionModel);
            TerminalService= new TerminalService(CommunicationModule, TerminalModel);
            OrientationService  = new OrientationService(CommunicationModule, OrientationModel);
            AllSensorsDataService = new AllSensorsDataService(CommunicationModule, AllSensorsDataModel);
            _services= new List<ServiceBase>{TerminalService,OrientationService, AllSensorsDataService};
            ConnectionModel.PropertyChanged += ConnectionModel_PropertyChanged;
        }

        void ConnectionModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName=="IsRunning")
            {
                if(!ConnectionModel.IsRunning)
                {
                    _services.ForEach(s=>s.Stop());
                }
            }
        }


        public ConnectionModel ConnectionModel { get; set; }

        public OrientationModel OrientationModel { get; set; }

        public AllSensorsDataModel AllSensorsDataModel { get; set; }

        public ApplicationStateModel ApplicationStatusModel { get; set; }

        public TerminalModel TerminalModel { get; set; }

        public CommunicationModule CommunicationModule { get; set; }

        public TerminalService TerminalService { get; set; }

        public OrientationService OrientationService { get; set; }

        public AllSensorsDataService AllSensorsDataService { get; set; }

        private void ApplicationStatusModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _services.ForEach(p => p.Stop());

            switch (ApplicationStatusModel.ApplicationState)
            {
                case ApplicationState.Terminal:
                    break;
                case ApplicationState.AllSensorsChart:
                    break;
                case ApplicationState.CompassChart:
                    break;
                case ApplicationState.OrientationChart:
                    break;
                case ApplicationState.Connection:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
