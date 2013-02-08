using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.Client.Models
{
    class MainModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ConnectionModel ConnectionModel { get; set; }

        public SensorsDataModel SensorsDataModel { get; set; }

        public OrientationModel OrientationModel { get; set; }

        public ConfigurationModel ConfigurationModel { get; set; }

        public ApplicationStatusModel ApplicationStatusModel { get; set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
