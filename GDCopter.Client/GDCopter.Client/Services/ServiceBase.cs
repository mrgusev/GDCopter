using System.ComponentModel;
using GDCopter.Client.Models;

namespace GDCopter.Client.Services
{
    public class ServiceBase
    {
        protected bool IsRunning = false;

        protected ModelBase Model { get; private set; }

        protected CommunicationModule CommunicationModule { get; private set; }

        protected ServiceBase(CommunicationModule communicationModule, ModelBase model)
        {
            Model = model;
            CommunicationModule = communicationModule;
            Model.PropertyChanged+=ModelPropertyChanged;
        }

        protected float ParseValue(string value)
        {
            float result;
            if (float.TryParse(value.Replace('.', ','), out result))
            {
                return result;
            }
            return 0;
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsRunning")
            {
                if(Model.IsRunning && !IsRunning)
                {
                    Run();
                }
                if (!Model.IsRunning && IsRunning)
                {
                    Stop();
                }
            }
        }

        public virtual void Run()
        {
            IsRunning = true;
            Model.IsRunning = true;
        }

        public virtual void Stop()
        {
            IsRunning = false;
            Model.IsRunning = false;
        }
    }
}
