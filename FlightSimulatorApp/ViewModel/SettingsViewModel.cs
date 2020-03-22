using FlightSimulator.ViewModel;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModel
{
    public class SettingsViewModel : Notify
    {
        private ISettings model;
        private Window window;

        public SettingsViewModel(Window aWindow)
        {
            this.model = new SettingsModel();
            this.window = aWindow;
        }

        public string ServerIP
        {
            get { return model.ServerIP; }
            set
            {
                model.FlightServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }

        public int ServerPort
        {
            get { return model.ServerPort; }
            set
            {
                model.FlightCommandPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }


        public void SaveSettings()
        {
            model.SaveSettings();
        }

        public void ReloadSettings()
        {
            model.ReloadSettings();
        }

        //add commands!
       
    }
}