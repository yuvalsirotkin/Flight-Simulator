using FlightSimulatorApp.Model;
using FlightSimulator.Model;
using FlightSimulator.ViewModel;
using FlightSimulatorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlightSimulator.Model
{
    public class SettingsModel : ISettings
    {
        #region Singleton
        private static ISettings m_Instance = null;
        public static ISettings Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new SettingsModel();
                }
                return m_Instance;
            }
        }
        #endregion
        public string ServerIP
        {
            get { return Properties.Settings.Default.ServerIP; }
            set { Properties.Settings.Default.ServerIP = value; }
        }
        public int ServerPort
        {
            get { return Properties.Settings.Default.ServerPort; }
            set { Properties.Settings.Default.ServerPort = value; }
        }


        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }

        public void ReloadSettings()
        {
            Properties.Settings.Default.Reload();
        }
    }
}