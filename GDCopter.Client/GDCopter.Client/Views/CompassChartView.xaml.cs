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
    /// Interaction logic for CompassChartView.xaml
    /// </summary>
    public partial class CompassChartView : UserControl
    {
        public CompassChartView()
        {
            InitializeComponent();
            plotter.LegendVisible = false;
            //plotter2.LegendVisible = false;
            //plotter3.LegendVisible = false;
            horizontalAxis.ShowMayorLabels = false;
            //dateAxis2.ShowMayorLabels = false;
            //dateAxis3.ShowMayorLabels = false;
            DataContextChanged += CompassChartViewDataContextChanged;
        }

        private void CompassChartViewDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((ModelBase)DataContext).PropertyChanged += CompassChartViewPropertyChanged;
        }


        private void CompassChartViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Data")
            {
                UpdateChart();
            }
        }

        private void UpdateChart()
        {
            var model = (CompassDataModel)DataContext;

            var source = new EnumerableDataSource<Point>(model.Points);
            source.SetXMapping(p => p.X);
            source.SetYMapping(p => p.Y);
            linegraph1.DataSource = source;
            plotter.Viewport.FitToView();
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            var values = ((ModelBase)DataContext);
            values.IsRunning = !values.IsRunning;
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            var model = ((CompassDataModel) DataContext);
            model.Points.Clear();
            linegraph1.DataSource = new EmptyDataSource();
        }
    }
}
