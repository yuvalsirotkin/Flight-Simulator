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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Threading;
using System.IO;
using System.Net.Sockets;

namespace FlightSimulatorApp
{
    // Interaction logic for Home.xaml
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


        //Default command- takes the ip and port from app config
        private void DefaultCommand(object sender, RoutedEventArgs e)
        {
            this.ip = ConfigurationManager.AppSettings["ServerIP"];  //127.0.0.1
            this.port = ConfigurationManager.AppSettings["ServerPort"]; //5402 
            IP.Text = this.ip;
            Port.Text = this.port;
        }

        // set the parameters we got
        private void SetCommand(object sender, RoutedEventArgs e)
        {
            this.ip = ServerIP;
            this.port = ServerPort;
            if (ip != "" && port != "")
            {
                string message = String.Format("IP & Port is Set");
                ErrMsg.Text = message;
            }
        }

        private Stopwatch stopWatch;
        //fly command- try to connect tje server
        private void FlyCommand(object sender, RoutedEventArgs e)
        {
            if (ip == "" && port == "")
            {
                string message = String.Format("You must enter IP & Port adress");
                ErrMsg.Text = message;
            }

            else if (ip == "")
            {
                string message = String.Format("You must enter IP adress");
                ErrMsg.Text = message;
            }
            else if (port == "")
            {
                string message = String.Format("You must enter Port adress");
                ErrMsg.Text = message;
            }
            else { 
                try // to connect
                {

                    portInInt = int.Parse(port);
                    SimulatorView simulatorView = new SimulatorView(this.ip, portInInt, this);
                    this.NavigationService.Navigate(simulatorView);
                } 
                catch
                // any error that appears
                {

                    string message = String.Format("Not able to connet, Something went wrong");
                    ErrMsg.Text = message;
                }
            }
        }

        // exit the program
        private void ExitCommand(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
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
