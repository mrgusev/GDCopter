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
            ConnectionModel = new ConnectionModel();
            ConfigurationModel = new ConfigurationModel();
            SensorsDataModel = new SensorsDataModel();
            OrientationModel = new OrientationModel();
            ApplicationStatusModel = new ApplicationStatusModel{IsControl = true};
            CommunicationService = new CommunicationService(ConnectionModel);
            TestService = new TestService();
            TerminalModel = new TerminalModel();
            TerminalService = new TerminalService(CommunicationService, TerminalModel);
        }
        public CommunicationService CommunicationService { get; set; }

        public TerminalService TerminalService { get; set; }

        public TestService TestService { get; set; }

        public StatisticService StatisticService { get; set; }

        public ConnectionModel ConnectionModel { get; set; }

        public SensorsDataModel SensorsDataModel { get; set; }

        public OrientationModel OrientationModel { get; set; }

        public ConfigurationModel ConfigurationModel { get; set; }

        public ApplicationStatusModel ApplicationStatusModel { get; set; }

        public TerminalModel TerminalModel { get; set; }

    }
}
