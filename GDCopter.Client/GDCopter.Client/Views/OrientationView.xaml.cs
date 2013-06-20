using System;
using System.Collections.Specialized;
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
    public partial class OrientationView : UserControl
    {
        public OrientationView()
        {
            InitializeComponent();
            plotter.Legend.LegendLeft = 10;
            plotter.Legend.LegendTop = 10;
            plotter.LegendVisible = false;
            dateAxis1.ShowMayorLabels = false;
            linegraph1.Description = new PenDescription("Pitch");
            linegraph2.Description = new PenDescription("Yaw");
            linegraph3.Description = new PenDescription("Roll");
            DataContextChanged += OrientationViewDataContextChanged;
        }

        private void OrientationViewDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((OrientationModel)DataContext).OrientationValues.CollectionChanged+=OrientationValuesCollectionChanged;
        }


        private void OrientationValuesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateChart();
        }

        private void UpdateChart()
        {
            var values = ((OrientationModel) DataContext).OrientationValues;
            
            var datesDataSource = new EnumerableDataSource<DateTime>(values.Select(d => d.Time));
            datesDataSource.SetXMapping(x => dateAxis1.ConvertToDouble(x));

            var numberXDataSource = new EnumerableDataSource<double>(values.Select(p => p.X * 57.295));
            numberXDataSource.SetYMapping(y => y);

            var numberYDataSource = new EnumerableDataSource<double>(values.Select(p => p.Y * 57.295));
            numberYDataSource.SetYMapping(y => y);

            var numberZDataSource = new EnumerableDataSource<double>(values.Select(p => p.Z * 57.295));
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

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            var values = ((OrientationModel)DataContext);
            values.IsRunning = !values.IsRunning;

        }
    }
}
