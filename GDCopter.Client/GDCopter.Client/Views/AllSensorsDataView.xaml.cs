using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GDCopter.Client.Models;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace GDCopter.Client.Views
{
    /// <summary>
    /// Interaction logic for AllSensorsDataView.xaml
    /// </summary>
    public partial class AllSensorsDataView : UserControl
    {
        public AllSensorsDataView()
        {
            InitializeComponent();
            DataContextChanged += AllSensorsDataViewDataContextChanged;
            plotter.Legend.LegendLeft = 10;
            plotter.Legend.LegendTop = 10;
            plotter2.Legend.LegendLeft = 10;
            plotter2.Legend.LegendTop = 10;
            plotter3.Legend.LegendLeft = 10;
            plotter3.Legend.LegendTop = 10;
            plotter4.Legend.LegendLeft = 10;
            plotter4.Legend.LegendTop = 10;
            //plotter.LegendVisible = false;
            //plotter2.LegendVisible = false;
            //plotter3.LegendVisible = false;
            dateAxis1.ShowMayorLabels = false;
            dateAxis2.ShowMayorLabels = false;
            dateAxis3.ShowMayorLabels = false;
            dateAxis4.ShowMayorLabels = false;
            linegraph1.Description = new PenDescription("X");
            linegraph2.Description = new PenDescription("Y");
            linegraph3.Description = new PenDescription("Z");
            linegraph4.Description = new PenDescription("X");
            linegraph5.Description = new PenDescription("Y");
            linegraph6.Description = new PenDescription("Z");
            linegraph7.Description = new PenDescription("X");
            linegraph8.Description = new PenDescription("Y");
            linegraph9.Description = new PenDescription("Z");
            linegraph10.Description = new PenDescription("Height");

        }

        private void AllSensorsDataViewDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((AllSensorsDataModel)DataContext).PropertyChanged += AllSensorsDataViewPropertyChanged;
        }

        private void AllSensorsDataViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Data")
            {
                UpdateCharts();
            }
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            var values = ((AllSensorsDataModel)DataContext);
            values.IsRunning = !values.IsRunning;
        }

        private void UpdateCharts()
        {
            UpdateGyro();
            UpdateAccell();
            UpdateMag();
            UpdateBaro();
        }

        private void UpdateBaro()
        {
            var model = (AllSensorsDataModel)DataContext;
            var datesDataSource = new EnumerableDataSource<DateTime>(model.GyroValues.Select(d => d.Time));
            datesDataSource.SetXMapping(x => dateAxis1.ConvertToDouble(x));

            var numberXDataSource = new EnumerableDataSource<double>(model.PressureValuse.Select(p => p.Value));
            numberXDataSource.SetYMapping(y => y);

            CompositeDataSource compositeDataSource1 = new
              CompositeDataSource(datesDataSource, numberXDataSource);

            linegraph10.DataSource = compositeDataSource1;
            plotter4.Viewport.FitToView();
        }

        void UpdateGyro()
        {
            var model = (AllSensorsDataModel)DataContext;
            var datesDataSource = new EnumerableDataSource<DateTime>(model.GyroValues.Select(d => d.Time));
            datesDataSource.SetXMapping(x => dateAxis1.ConvertToDouble(x));

            var numberXDataSource = new EnumerableDataSource<double>(model.GyroValues.Select(p => p.X));
            numberXDataSource.SetYMapping(y => y);

            var numberYDataSource = new EnumerableDataSource<double>(model.GyroValues.Select(p => p.Y));
            numberYDataSource.SetYMapping(y => y);

            var numberZDataSource = new EnumerableDataSource<double>(model.GyroValues.Select(p => p.Z));
            numberZDataSource.SetYMapping(y => y);

            CompositeDataSource compositeDataSource1 = new
              CompositeDataSource(datesDataSource, numberXDataSource);
            CompositeDataSource compositeDataSource2 = new
              CompositeDataSource(datesDataSource, numberYDataSource);
            CompositeDataSource compositeDataSource3 = new
              CompositeDataSource(datesDataSource, numberZDataSource);

            linegraph1.DataSource = compositeDataSource1;
            linegraph2.DataSource = compositeDataSource2;
            linegraph3.DataSource = compositeDataSource3;
            plotter.Viewport.FitToView();

        }

        void UpdateAccell()
        {
            var model = (AllSensorsDataModel)DataContext;
            var datesDataSource = new EnumerableDataSource<DateTime>(model.AccelValues.Select(d => d.Time));
            datesDataSource.SetXMapping(x => dateAxis2.ConvertToDouble(x));

            var numberXDataSource = new EnumerableDataSource<double>(model.AccelValues.Select(p => p.X));
            numberXDataSource.SetYMapping(y => y);

            var numberYDataSource = new EnumerableDataSource<double>(model.AccelValues.Select(p => p.Y));
            numberYDataSource.SetYMapping(y => y);

            var numberZDataSource = new EnumerableDataSource<double>(model.AccelValues.Select(p => p.Z));
            numberZDataSource.SetYMapping(y => y);

            CompositeDataSource compositeDataSource1 = new
              CompositeDataSource(datesDataSource, numberXDataSource);
            CompositeDataSource compositeDataSource2 = new
              CompositeDataSource(datesDataSource, numberYDataSource);
            CompositeDataSource compositeDataSource3 = new
              CompositeDataSource(datesDataSource, numberZDataSource);

            linegraph4.DataSource = compositeDataSource1;
            linegraph5.DataSource = compositeDataSource2;
            linegraph6.DataSource = compositeDataSource3;
            plotter2.Viewport.FitToView();
        }

        void UpdateMag()
        {
            var model = (AllSensorsDataModel)DataContext;
            var datesDataSource = new EnumerableDataSource<DateTime>(model.CompassValues.Select(d => d.Time));
            datesDataSource.SetXMapping(x => dateAxis3.ConvertToDouble(x));

            var numberXDataSource = new EnumerableDataSource<double>(model.CompassValues.Select(p => p.X));
            numberXDataSource.SetYMapping(y => y);

            var numberYDataSource = new EnumerableDataSource<double>(model.CompassValues.Select(p => p.Y));
            numberYDataSource.SetYMapping(y => y);

            var numberZDataSource = new EnumerableDataSource<double>(model.CompassValues.Select(p => p.Z));
            numberZDataSource.SetYMapping(y => y);

            CompositeDataSource compositeDataSource1 = new
              CompositeDataSource(datesDataSource, numberXDataSource);
            CompositeDataSource compositeDataSource2 = new
              CompositeDataSource(datesDataSource, numberYDataSource);
            CompositeDataSource compositeDataSource3 = new
              CompositeDataSource(datesDataSource, numberZDataSource);

            linegraph7.DataSource = compositeDataSource1;
            linegraph8.DataSource = compositeDataSource2;
            linegraph9.DataSource = compositeDataSource3;
            plotter3.Viewport.FitToView();
        }
    }
}
