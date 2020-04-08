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

namespace FlightSimulatorApp.View
{
    /// <summary>
    /// Interaction logic for Navigators.xaml
    /// </summary>
    public partial class Navigators : UserControl
    {
        SimViewModel vm;
        public Navigators()
        {
            InitializeComponent();
            
        }

        public void set(SimViewModel svm)
        {
            this.vm = svm;
            DataContext = vm;
        }

        private void joystick1_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
