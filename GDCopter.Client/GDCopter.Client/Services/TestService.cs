using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDCopter.Client.Models;

namespace GDCopter.Client.Services
{
    class TestService
    {
        public TestService()
        {
            RotorTestModel = new RotorTestModel();
            RotorTestModel.PropertyChanged += RotorTestModelPropertyChanged;
        }

        public CommunicationService CommunicationService { get; set; }

        public SensorsDataModel SensorsDataModel { get; set; }

        public RotorTestModel RotorTestModel { get; private set; }

        public void SwitchOnSensorsTestMode()
        {

        }

        public void SwitchOnRotorTestMode()
        {

        }

        public void SwitchOffTestMode()
        {
            
        }

        private void RotorTestModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

    }
}
