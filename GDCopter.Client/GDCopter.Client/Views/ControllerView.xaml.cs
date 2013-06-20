using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GDCopter.Client.Models;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace GDCopter.Client.Views
{
    /// <summary>
    /// Interaction logic for OrientationView.xaml
    /// </summary>
    public partial class ControllerView : UserControl
    {
        public ControllerView()
        {
            InitializeComponent();
            plotter.Legend.LegendLeft = 10;
            plotter.Legend.LegendTop = 10;
            plotter.LegendVisible = false;
            plotter1.Legend.LegendLeft = 10;
            plotter1.Legend.LegendTop = 10;
            plotter1.LegendVisible = false;
            dateAxis1.ShowMayorLabels = false;
            dateAxis2.ShowMayorLabels = false;
            linegraph1.Description = new PenDescription("w1");
            linegraph2.Description = new PenDescription("w2");
            linegraph3.Description = new PenDescription("w3");
            linegraph4.Description = new PenDescription("w4");
            linegraph5.Description = new PenDescription("Throttle");
            DataContextChanged += ControllerViewDataContextChanged;
        }

        private void ControllerViewDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((ControllerModel)DataContext).PropertyChanged+= OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateChart();
        }

        private void UpdateChart()
        {
            var rotor1 = ((ControllerModel)DataContext).Rotor1;
            var rotor2 = ((ControllerModel)DataContext).Rotor2;
            var rotor3 = ((ControllerModel)DataContext).Rotor3;
            var rotor4 = ((ControllerModel)DataContext).Rotor4;

            var throttle = ((ControllerModel)DataContext).Throttle;

            var datesDataSource = new EnumerableDataSource<DateTime>(rotor1.Select(d => d.Time));
            datesDataSource.SetXMapping(x => dateAxis1.ConvertToDouble(x));

            var r1DataSource = new EnumerableDataSource<double>(rotor1.Select(p => p.Value));
            r1DataSource.SetYMapping(y => y);

            var r2DataSource = new EnumerableDataSource<double>(rotor2.Select(p => p.Value));
            r2DataSource.SetYMapping(y => y);

            var r3DataSource = new EnumerableDataSource<double>(rotor3.Select(p => p.Value));
            r3DataSource.SetYMapping(y => y);

            var r4DataSource = new EnumerableDataSource<double>(rotor4.Select(p => p.Value));
            r4DataSource.SetYMapping(y => y);

            var throttleDataSource = new EnumerableDataSource<double>(throttle.Select(p => p.Value));
            throttleDataSource.SetYMapping(y => y);

            CompositeDataSource compositeDataSource1 = new
              CompositeDataSource(datesDataSource, r1DataSource);
            CompositeDataSource compositeDataSource2 = new
              CompositeDataSource(datesDataSource, r2DataSource);
            CompositeDataSource compositeDataSource3 = new
              CompositeDataSource(datesDataSource, r3DataSource);
            CompositeDataSource compositeDataSource4 = new
              CompositeDataSource(datesDataSource, r4DataSource);
            CompositeDataSource compositeDataSource5 = new
              CompositeDataSource(datesDataSource, throttleDataSource);

            linegraph1.DataSource = compositeDataSource1;
            linegraph2.DataSource = compositeDataSource2;
            linegraph3.DataSource = compositeDataSource3;
            linegraph4.DataSource = compositeDataSource4;
            linegraph5.DataSource = compositeDataSource5;
            plotter.Viewport.FitToView();
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            var values = ((ControllerModel)DataContext);
            values.IsRunning = !values.IsRunning;

        }
    }
}
