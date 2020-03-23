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
            this.vm = new SimViewModel(new NavigatorModel(new TCPSet(), ip, port), new MapAndDashboardModel(new TCPGet(port, ip), ip, port));
            InitializeComponent();
        }
    }
}
