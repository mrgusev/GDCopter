using System.Collections.Generic;

namespace GDCopter.Client.Models
{
    public class AllSensorsDataModel : ModelBase
    {
        public List<StatisticPoint> GyroValues { get; private set; }

        public List<StatisticPoint> AccelValues { get; private set; }

        public List<StatisticPoint> CompassValues { get; private set; }

        public List<SimpleStatisticPoint> PressureValuse { get; private set; } 

        public AllSensorsDataModel()
        {
            GyroValues = new List<StatisticPoint>();
            AccelValues = new List<StatisticPoint>();
            CompassValues = new List<StatisticPoint>();
            PressureValuse = new List<SimpleStatisticPoint>();
        }

        public void Update()
        {
            OnPropertyChanged("Data");
        }
    }
}
