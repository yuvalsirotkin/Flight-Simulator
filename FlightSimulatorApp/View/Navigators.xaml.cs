using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Navigators : UserControl, INotifyPropertyChanged
    {
        SimViewModel vm;
        private double throttle, elevator, rudder, aileron;
        public event PropertyChangedEventHandler PropertyChangedNotify;
        public event PropertyChangedEventHandler PropertyChanged;
        string throttleText;
        public Navigators()
        {
            InitializeComponent();       
        }

        public void set(SimViewModel svm)
        {
            this.vm = svm;
            vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                this.NotifyPropertyChanged(e.PropertyName);
            };
            DataContext = vm;
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        //public void NotifyPropertyChanged(string propertyName, object newValue)
        //{
        //    if (this.PropertyChangedNotify != null)
        //        PropertyChangedNotify(this, new PropertyChangedExtendedEventArgs(propertyName, newValue));
        //}

        public double Rudder
        {
            get { return this.rudder; }
            set
            {
                this.rudder = value;
                //this.NotifyPropertyChanged("Rudder", value);
          
                this.NotifyPropertyChanged("RudderText");

            }
        }
        public double Elevator
        {
            get { return this.elevator; }
            set
            {
                this.elevator = value;
                //this.NotifyPropertyChanged("Elevator", value);
                this.NotifyPropertyChanged("ElevatorText");

            }
        }
        public double Aileron
        {
            get { return this.aileron; }
            set
            {

                this.aileron = value;
                //this.NotifyPropertyChanged("Aileron", value);
                this.NotifyPropertyChanged("AileronText");

            }
        }
        public double Throttle
        {
            get { return this.throttle; }
            set
            {
                this.throttle = value;
                //this.NotifyPropertyChanged("Throttle", value);
                this.vm.NotifyPropertyChanged("Throttle");
                this.throttleText = "" + value;
                //this.NotifyPropertyChanged("ThrottleText");
            }
        }

        public string RudderText
        {
            get { return String.Format("{0:0.000}", Rudder); }
        }

        public string ThrottleText
        {
            get
            {
                return String.Format("{0:0.000}", Throttle);
            }
        }

        public string AileronText
        {
            get { return String.Format("{0:0.000}", Aileron); }
        }

        public string ElevatorText
        {
            get { return String.Format("{0:0.000}", elevator); }
        }

        private void joystick1_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
