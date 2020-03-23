using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;

namespace FlightSimulatorApp.Model
{
    interface INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
    }
}

public delegate void PropertyChangedEventHandler(
Object sender,
System.ComponentModel.PropertyChangedEventArgs e
);

