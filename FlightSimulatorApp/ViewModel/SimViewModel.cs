using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp.ViewModel
{
    class SimViewModel
    {
        private MapAndDashboardModel mapAndDashboardModel;
        private NavigatorModel navigatorModel;

        private double throttle;
        private double elavetor;
        private double rudder;
        private double aileron;
        //properties
        // changes in the properties (from the view) will change the navigator model
        public double VM_Elavetor
        {
            get { return this.elavetor; }
            set
            {
                this.elavetor = value;
                navigatorModel.Elavetor = value;
            }
        }
        public double VM_Throttle
        {
            get { return this.throttle; }
            set
            {
                this.throttle = value;
                navigatorModel.Throttle = value;
            }
        }
        public double VM_Aileron
        {
            get { return this.aileron; }
            set
            {
                this.aileron = value;
                navigatorModel.Aileron = value;
            }
        }
        public double VM_Rudder
        {
            get { return this.rudder; }
            set
            {
                this.rudder = value;
                navigatorModel.Rudder = value;
            }
        }
        //public Location VM_Location
        //{
        //    get
        //    {
        //       // return model.Location;
        //    }
        //}
        public SimViewModel(NavigatorModel navigatorModel, MapAndDashboardModel mapAndDashboardModel)
        {
            this.navigatorModel = navigatorModel;
            this.mapAndDashboardModel = mapAndDashboardModel;
            // when proprety changed in the model- it will notify the VM also
            // use the map and dashboard model
            mapAndDashboardModel.PropertyChanged +=  (object sender, System.ComponentModel.PropertyChangedEventArgs e)=> {
                Model.PropertyChangedEventArgs e1 = Model.PropertyChangedEventArgs.converPropertyChangedEventArgs(e);
                NotifyPropertyChanged("VM_" + e1.Name);
            };
        }



        public event PropertyChangedEventHandler PropertyChanged;

        // the propertyChanged should contain function that will change the view when the vm changed
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                Model.PropertyChangedEventArgs e = new Model.PropertyChangedEventArgs();
                e.Name = propName;
                this.PropertyChanged(this, e);
            }
        }
    }
}
