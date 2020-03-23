using FlightSimulatorApp.Model;
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
        private SettingsModel()
        {
        }
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
            get { return FlightSimulatorApp.Properties.Settings.Default.ServerIP; }
            set { FlightSimulatorApp.Properties.Settings.Default.ServerIP = value; }
        }
        public int ServerPort
        {
            get { return 5042; } //!!!!!!!!!!!!!!
            set { FlightSimulatorApp.Properties.Settings.Default.ServerPort = value.ToString(); }
        }


        public void SaveSettings()
        {
            FlightSimulatorApp.Properties.Settings.Default.Save();
        }

        public void ReloadSettings()
        {
            FlightSimulatorApp.Properties.Settings.Default.Reload();
        }
    }
}