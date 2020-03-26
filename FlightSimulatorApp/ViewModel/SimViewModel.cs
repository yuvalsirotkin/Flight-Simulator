using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp.ViewModel
{
    class SimViewModel
    {
        private MapAndDashboardModel mapAndDashboardModel;
        private NavigatorModel navigatorModel;

        /*private double throttle;
        private double elavetor;
        private double rudder;
        private double aileron;*/

        private double headingDeg;
        private double verticalSpeed;
        private double airSpeed;
        private double roll;
        private double pitch;
        private double altitude;
        private double altimeter;
        private double groundSpeed;
        
        //properties
        // changes in the properties (from the view) will change the navigator model
        public double VM_Elavetor
        {
            /*get { return this.elavetor; }*/
            set
            {
                /*this.elavetor = value;*/
                navigatorModel.Elavetor = value;
            }
        }
        public double VM_Throttle
        {
            /*get { return this.throttle; }*/
            set
            {
                /*this.throttle = value;*/
                navigatorModel.Throttle = value;
            }
        }
        public double VM_Aileron
        {
            /*get { return this.aileron; }*/
            set
            {
                /*this.aileron = value;*/
                navigatorModel.Aileron = value;
            }
        }
        public double VM_Rudder
        {
            /*get { return this.rudder; }*/
            set
            {
                /*this.rudder = value;*/
                navigatorModel.Rudder = value;
            }
        }


        public double VM_HeadingDeg
        {
            get { return this.headingDeg; }
            set
            {
                this.headingDeg = value;
            }
        }

        public double VM_VerticalSpeed
        {
            get { return this.verticalSpeed; }
            set
            {
                this.verticalSpeed = value;
            }
        }
        public double VM_GroundSpeed
        {
            get { return this.groundSpeed; }
            set
            {
                this.groundSpeed = value;
            }
        }
        public double VM_Airspeed
        {
            get { return this.airSpeed; }
            set
            {
                this.airSpeed = value;
            }
        }
        public double VM_Altitude
        {
            get { return this.altitude; }
            set
            {
                this.altitude = value;
            }
        }
        public double VM_Roll
        {
            get { return this.roll; }
            set
            {
                this.roll = value;
            }
        }
        public double VM_Pitch
        {
            get { return this.pitch; }
            set
            {
                this.pitch = value;
            }
        }
        public double VM_Altimeter
        {
            get { return this.altimeter; }
            set
            {
                this.altimeter = value;
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
            mapAndDashboardModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                switch(e.Name)
                {
                    case "headingDeg":
                        this.headingDeg = e.Val;
                        break;
                    case "veritcalSpeed":
                        this.VM_VerticalSpeed = e.Val;
                        break;
                    case "groundSpeed":
                        this.VM_GroundSpeed = e.Val;
                        break;
                    case "airSpeed":
                        this.VM_Airspeed = e.Val;
                        break;
                    case "altitude":
                        this.VM_Altitude= e.Val;
                        break;
                    case "roll":
                        this.VM_Roll = e.Val;
                        break;
                    case "pitch":
                        this.VM_Pitch = e.Val;
                        break;
                    case "altimeter":
                        this.VM_Altimeter = e.Val;
                        break;

                }
            };
        }

        //public SimViewModel(NavigatorModel navigatorModel, MapAndDashboardModel mapAndDashboardModel)
        //{
        //    this.navigatorModel = navigatorModel;
        //    this.mapAndDashboardModel = mapAndDashboardModel;
        //    // when proprety changed in the model- it will notify the VM also
        //    // use the map and dashboard model
        //    mapAndDashboardModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
        //        NotifyPropertyChanged(e);
        //    };
        //}


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
