using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private string ip = "";
        private string port = "";
        private int portInInt = 0;
        public Home()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void DefaultCommand(object sender, RoutedEventArgs e)
        {
            this.ip = "127.0.0.1";
            this.port = "5402";
            IP.Text = this.ip;
            Port.Text = this.port;
        }

        private void SetCommand(object sender, RoutedEventArgs e)
        {
            this.ip = ServerIP;
            this.port = ServerPort;
        }
        private void FlyCommand(object sender, RoutedEventArgs e)
        {
 
            if (ip == "")
            {
                // return ERR
            }
            if (port == "")
            {
                // return ERR
            }
            portInInt = int.Parse(port);
            SimulatorView simulatorView = new SimulatorView(this.ip, portInInt);
            this.NavigationService.Navigate(simulatorView);
        }
        public string ServerIP
        {
            get { return this.ip; }
            set { this.ip = value; }
        }
        public string ServerPort
        {
            get { return this.port; }
            set { this.port = value; }
        }
    }
}
