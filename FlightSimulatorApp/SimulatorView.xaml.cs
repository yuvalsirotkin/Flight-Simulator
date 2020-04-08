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
using FlightSimulatorApp.ViewModel;
using FlightSimulatorApp.Model;
using System.Net.Sockets;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for SimulatorView.xaml
    /// </summary>
   
    public partial class SimulatorView : Page
    {
        private SimViewModel vm;
        public SimulatorView(string ip, int port)
        {
            TcpGetSet tcpConnection = new TcpGetSet();
            tcpConnection.connect(ip, port);
            this.vm = new SimViewModel(new NavigatorModel(tcpConnection), new MapAndDashboardModel(tcpConnection));
            //this.vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
            //    Console.WriteLine("should change the dashboard");
            //};
            InitializeComponent();
            DataContext = vm;
            navigators.set(vm);
        }

        private bool firstTime = true;
        private void pin_LayoutUpdated(object sender, EventArgs e)
        {
            if (pin.Location != null)
            {
                double latitude = pin.Location.Latitude;
                double longtitude = pin.Location.Longitude;
                Console.WriteLine("in the pin");
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
        private void ExitCommand(object sender, RoutedEventArgs e)
        {
           // this.TcpGetSet.disconnect();
            System.Environment.Exit(0);
        }

        private void MoveToCenter(object sender, EventArgs e)
        {
            try
            {
                myMap.Center = pin.Location;
            }
            catch
            {
                //ErrorText.Text = "wasn't able to center the map";
                Console.WriteLine("wasn't able to center the map");
            }
        }

    }
}
