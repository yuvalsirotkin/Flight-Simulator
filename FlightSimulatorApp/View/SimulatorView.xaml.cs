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
using FlightSimulatorApp.View;
using FlightSimulatorApp.ViewModel;
using FlightSimulatorApp.Model;
using System.Net.Sockets;
using Microsoft.Maps.MapControl.WPF;
using System.ComponentModel;
using System.Threading;

namespace FlightSimulatorApp
{
    /// Interaction logic for SimulatorView.xaml
   
    public partial class SimulatorView : Page
    {
        private SimViewModel vm;
        private Mutex mut;
        MapAndDashboardModel myMapAndDash;
        private TcpGetSet tcpConnection;
        public SimulatorView(string ip, int port, Home home)
        {
            this.mut = new Mutex();
            //Create the connection to the simulator
            this.tcpConnection = new TcpGetSet();
            tcpConnection.connect(ip, port);
            myMapAndDash = new MapAndDashboardModel(tcpConnection, mut);
            //Create the view model
            this.vm = new SimViewModel(new NavigatorModel(tcpConnection, mut), myMapAndDash);
            tcpConnection.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                vm.NotifyPropertyChanged("serverProblem");
            };
            InitializeComponent();
            DataContext = vm;
        }

        public void afterSleep(Home home)
        {
            myMapAndDash = new MapAndDashboardModel(tcpConnection, mut);
            //Create the view model
            this.vm = new SimViewModel(new NavigatorModel(tcpConnection, mut), myMapAndDash);
            tcpConnection.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                vm.NotifyPropertyChanged("serverProblem");
            };
            InitializeComponent();
            DataContext = vm;
        }

        private bool firstTime = true;
        //Pin for the map.
        private void pin_LayoutUpdated(object sender, EventArgs e)
        {
            if (pin.Location != null)
            {
                double latitude = pin.Location.Latitude;
                double longtitude = pin.Location.Longitude;    
                if (firstTime)
                {
                    myMap.SetView(new Location(latitude, longtitude), 10);
                    PlainPosition.X = 0;
                    PlainPosition.Y = 0;
                    firstTime = false;
                    return;
                }
            }
        }

        //Exit - discoonect & exit from the application.
        private void ExitCommand(object sender, RoutedEventArgs e)
        {
            myMapAndDash.disconnect();
            System.Environment.Exit(0);
        }

        //Discoonect from the sim and return the the home page.
        private void Disconnect(object sender, RoutedEventArgs e)
        {
            myMapAndDash.disconnect();
            Home home = new Home();
            this.NavigationService.Navigate(home);
        }

        //Center the map.
        private void MoveToCenter(object sender, EventArgs e)
        {
            try
            {
                myMap.Center = pin.Location;
            }
            catch
            {
                vm.NotifyPropertyChanged("centerProblem");
            }
        }
    }
}
