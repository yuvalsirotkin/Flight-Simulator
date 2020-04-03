using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;

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
        public double Elavetor
        {
            get { return this.elavetor; }
            set
            {
                this.elavetor = value;
                navigatorModel.Elavetor = value;
            }
        }
        public double Throttle
        {
            get { return this.throttle; }
            set
            {
                this.throttle = value;
                navigatorModel.Throttle = value;
            }
        }
        public double Aileron
        {
            get { return this.aileron; }
            set
            {
                this.aileron = value;
                navigatorModel.Aileron = value;
            }
        }
        public double Rudder
        {
            get { return this.rudder; }
            set
            {
                this.rudder = value;
                navigatorModel.Rudder = value;
            }
        }

        public SimViewModel(NavigatorModel navigatorModel, MapAndDashboardModel mapAndDashboardModel)
        {
            this.navigatorModel = navigatorModel;
            this.mapAndDashboardModel = mapAndDashboardModel;
            // when proprety changed in the model- it will notify the VM also
            // use the map and dashboard model
            mapAndDashboardModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.Name);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // the propertyChanged should contain function that will change the view when the vm changed
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs();
                e.Name = propName;
                this.PropertyChanged(this, e);
            }
        }


        public string VM_LongitudeMsg
        {
            get
            {
                if (MapAndDashboardModel.Longitude == 180 || MapAndDashboardModel.Longitude == -180)
                {
                    return "Invalid Coordinate in Longitude";
                }
                return " ";
            }
        }
        public string VM_LatitudeMsg
        {
            get
            {
                if (MapAndDashboardModel.Latitude == 90 || MapAndDashboardModel.Latitude == -90)
                {
                    return "Invalid Coordinate in Latitude";
                }
                return " ";
            }
        }



        //public event PropertyChangedEventHandler PropertyChanged;

        //// the propertyChanged should contain function that will change the view when the vm changed
        //public void NotifyPropertyChanged(PropertyChangedEventArgs property)
        //{
        //    if (this.PropertyChanged != null)
        //    {
        //        Model.PropertyChangedEventArgs e = new Model.PropertyChangedEventArgs();
        //        e.Name = "VM_"+ property.Name;
        //        e.Val = property.Val;
        //        this.PropertyChanged(this, e);
        //    }
        //}

    }
}
