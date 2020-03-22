using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;

namespace FlightSimulatorApp.Model
{
    public interface ISettings
    {
        string ServerIP { get; set; }         
        int ServerPort { get; set; }           

        void SaveSettings();
        void ReloadSettings();
    }
}
