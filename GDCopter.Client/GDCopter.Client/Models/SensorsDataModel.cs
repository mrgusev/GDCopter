using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.Client.Models
{
    internal class SensorsDataModel : ModelBase
    {
        public SensorsDataModel()
        {
            GyroValues = new ObservableCollection<StatisticPoint>();
            AccellValues = new ObservableCollection<StatisticPoint>();
            CompassValues = new ObservableCollection<StatisticPoint>();
            GyroValues.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("GyroValues");
        }

        public ObservableCollection<StatisticPoint> GyroValues { get; set; }

        public ObservableCollection<StatisticPoint> AccellValues { get; set; }

        public ObservableCollection<StatisticPoint> CompassValues { get; set; }
    }
}
