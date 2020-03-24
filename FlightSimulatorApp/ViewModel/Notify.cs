using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;


namespace FlightSimulatorApp.ViewModel
{
    public abstract class Notify : Model.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            //this.PropertyChanged?.Invoke(this, Model.PropertyChangedEventArgs.propName);
        }
    }

}
