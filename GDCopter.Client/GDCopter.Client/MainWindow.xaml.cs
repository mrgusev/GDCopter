using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using GDCopter.Client.Models;
using GDCopter.Client.Services;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;

namespace GDCopter.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public CommunicationModule CommunicationModule { get; set; }

        public TerminalService TerminalService { get; set; }

        public OrientationService OrientationService { get; set; }

        public AllSensorsDataService AllSensorsDataService { get; set; }

        public CompassDataService CompassDataService { get; set; }

        public RotorsService RotorsService { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            var mainModel = new MainModel();
            CommunicationModule = new CommunicationModule(mainModel.ConnectionModel);
            TerminalService = new TerminalService(CommunicationModule, mainModel.TerminalModel);
            OrientationService = new OrientationService(CommunicationModule, mainModel.OrientationModel);
            AllSensorsDataService = new AllSensorsDataService(CommunicationModule, mainModel.AllSensorsDataModel);
            CompassDataService = new CompassDataService(CommunicationModule,mainModel.CompassDataModel);
            RotorsService = new RotorsService(CommunicationModule, mainModel.RotorsModel);
            mainModel.Services.AddMany(TerminalService, OrientationService, AllSensorsDataService,RotorsService);
            DataContext = mainModel;
        }
    }
}
