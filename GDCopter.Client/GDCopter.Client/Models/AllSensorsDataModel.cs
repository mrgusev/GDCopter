﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.Client.Models
{
    public class AllSensorsDataModel : ModelBase
    {
        public List<StatisticPoint> GyroValues { get; private set; }

        public List<StatisticPoint> AccelValues { get; private set; }

        public List<StatisticPoint> CompassValues { get; private set; }

        public AllSensorsDataModel()
        {
            GyroValues = new List<StatisticPoint>();
            AccelValues = new List<StatisticPoint>();
            CompassValues = new List<StatisticPoint>();
        }

        public void Update()
        {
            OnPropertyChanged("Data");
        }
    }
}
