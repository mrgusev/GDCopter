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
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace GDCopter.Client.Views
{
    /// <summary>
    /// Interaction logic for ChartsDataView.xaml
    /// </summary>
    public partial class ChartsDataView : UserControl
    {
        public ChartsDataView()
        {
            InitializeComponent();
            plotter.Legend.LegendLeft = 10;
            plotter.Legend.LegendTop = 10;
            plotter2.Legend.LegendLeft = 10;
            plotter2.Legend.LegendTop = 10;
            plotter3.Legend.LegendLeft = 10;
            plotter3.Legend.LegendTop = 10;
            //plotter.LegendVisible = false;
            //plotter2.LegendVisible = false;
            //plotter3.LegendVisible = false;
            dateAxis1.ShowMayorLabels = false;
            dateAxis2.ShowMayorLabels = false;
            dateAxis3.ShowMayorLabels = false;
            linegraph1.Description = new PenDescription("X");
            linegraph2.Description = new PenDescription("Y");
            linegraph3.Description = new PenDescription("Z");
            linegraph4.Description = new PenDescription("X");
            linegraph5.Description = new PenDescription("Y");
            linegraph6.Description = new PenDescription("Z");
            linegraph7.Description = new PenDescription("X");
            linegraph8.Description = new PenDescription("Y");
            linegraph9.Description = new PenDescription("Z");
           
        }
        //void UpdateGyro()
        //{
        //    var datesDataSource = new EnumerableDataSource<DateTime>(GyroPoints.Select(d => d.Time));
        //    datesDataSource.SetXMapping(x => dateAxis1.ConvertToDouble(x));

        //    var numberXDataSource = new EnumerableDataSource<double>(GyroPoints.Select(p => p.X));
        //    numberXDataSource.SetYMapping(y => y);

        //    var numberYDataSource = new EnumerableDataSource<double>(GyroPoints.Select(p => p.Y));
        //    numberYDataSource.SetYMapping(y => y);

        //    var numberZDataSource = new EnumerableDataSource<double>(GyroPoints.Select(p => p.Z));
        //    numberZDataSource.SetYMapping(y => y);

        //    CompositeDataSource compositeDataSource1 = new
        //      CompositeDataSource(datesDataSource, numberXDataSource);
        //    CompositeDataSource compositeDataSource2 = new
        //      CompositeDataSource(datesDataSource, numberYDataSource);
        //    CompositeDataSource compositeDataSource3 = new
        //      CompositeDataSource(datesDataSource, numberZDataSource);

        //    linegraph1.DataSource = compositeDataSource1;
        //    linegraph2.DataSource = compositeDataSource2;
        //    linegraph3.DataSource = compositeDataSource3;
        //    plotter.Viewport.FitToView();

        //}
        //void UpdateAccell()
        //{

        //    var datesDataSource = new EnumerableDataSource<DateTime>(AccellPoints.Select(d => d.Time));
        //    datesDataSource.SetXMapping(x => dateAxis2.ConvertToDouble(x));

        //    var numberXDataSource = new EnumerableDataSource<double>(AccellPoints.Select(p => p.X));
        //    numberXDataSource.SetYMapping(y => y);

        //    var numberYDataSource = new EnumerableDataSource<double>(AccellPoints.Select(p => p.Y));
        //    numberYDataSource.SetYMapping(y => y);

        //    var numberZDataSource = new EnumerableDataSource<double>(AccellPoints.Select(p => p.Z));
        //    numberZDataSource.SetYMapping(y => y);

        //    CompositeDataSource compositeDataSource1 = new
        //      CompositeDataSource(datesDataSource, numberXDataSource);
        //    CompositeDataSource compositeDataSource2 = new
        //      CompositeDataSource(datesDataSource, numberYDataSource);
        //    CompositeDataSource compositeDataSource3 = new
        //      CompositeDataSource(datesDataSource, numberZDataSource);

        //    linegraph4.DataSource = compositeDataSource1;
        //    linegraph5.DataSource = compositeDataSource2;
        //    linegraph6.DataSource = compositeDataSource3;
        //    plotter2.Viewport.FitToView();
        //}
        //void UpdateMag()
        //{

        //    var datesDataSource = new EnumerableDataSource<DateTime>(CompassPoints.Select(d => d.Time));
        //    datesDataSource.SetXMapping(x => dateAxis3.ConvertToDouble(x));

        //    var numberXDataSource = new EnumerableDataSource<double>(CompassPoints.Select(p => p.X));
        //    numberXDataSource.SetYMapping(y => y);

        //    var numberYDataSource = new EnumerableDataSource<double>(CompassPoints.Select(p => p.Y));
        //    numberYDataSource.SetYMapping(y => y);

        //    var numberZDataSource = new EnumerableDataSource<double>(CompassPoints.Select(p => p.Z));
        //    numberZDataSource.SetYMapping(y => y);

        //    CompositeDataSource compositeDataSource1 = new
        //      CompositeDataSource(datesDataSource, numberXDataSource);
        //    CompositeDataSource compositeDataSource2 = new
        //      CompositeDataSource(datesDataSource, numberYDataSource);
        //    CompositeDataSource compositeDataSource3 = new
        //      CompositeDataSource(datesDataSource, numberZDataSource);

        //    linegraph7.DataSource = compositeDataSource1;
        //    linegraph8.DataSource = compositeDataSource2;
        //    linegraph9.DataSource = compositeDataSource3;
        //    plotter3.Viewport.FitToView();
        //}
    }
}
