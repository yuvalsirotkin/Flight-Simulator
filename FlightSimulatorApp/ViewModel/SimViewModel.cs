﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp.ViewModel
{
    class SimViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private MapAndDashboardModel mapAndDashboardModel;
        private NavigatorModel navigatorModel;

        /*
        private double elavetor;
        private double rudder;
        private double aileron;*/
        //remove it
        private double throttle;
        //until here
        private double headingDeg;
        private double verticalSpeed;
        private double airSpeed;
        private double roll;
        private double altitude;
        private double altimeter;
        private double pitch;
        private double groundSpeed;
        private Location location;
        private double latitude = 50;
        private double longitude = 10;

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
            get { return this.throttle; }
            set
            {
                // should delete it
                this.throttle = value;
                //until here
                
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
            get {
                Console.WriteLine("get the heading deg is");
                return this.headingDeg;
            }
            set
            {
                this.headingDeg = value;
                Console.Write("the heading deg is");
                Console.WriteLine(this.headingDeg);
                OnPropertyChanged(new PropertyChangedEventArgs("VM_HeadingDeg"));
            }
        }


        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }


        public double VM_VerticalSpeed
        {
            get { return this.verticalSpeed; }
            set
            {
                this.verticalSpeed = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VM_VerticalSpeed"));
            }
        }
        public double VM_GroundSpeed
        {
            get { return this.groundSpeed; }
            set
            {
                this.groundSpeed = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VM_GroundSpeed"));
            }
        }
        public double VM_Airspeed
        {
            get { return this.airSpeed; }
            set
            {
                this.airSpeed = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VM_Airspeed"));
            }
        }
        public double VM_Altitude
        {
            get { return this.altitude; }
            set
            {
                this.altitude = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VM_Altitude"));
            }
        }
        public double VM_Roll
        {
            get { return this.roll; }
            set
            {
                this.roll = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VM_Roll"));
            }
        }

        public double VM_Pitch
        {
            get { return this.pitch; }
            set
            {
                this.pitch = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VM_Pitch"));
            }
        }

        public double VM_Altimeter
        {
            get { return this.altimeter; }
            set
            {
                this.altimeter = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VM_Altimeter"));
            }
        }

        public double VM_Latitude
        {
            get { return this.latitude; }
            set
            {
                this.latitude = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VM_Latitude"));
            }
        }

        public double VM_Longitude
        {
            get { return this.longitude; }
            set
            {
                this.longitude = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VM_Longitude"));
            }
        }

        public Location VM_Location
        {
            get
            {
                return new Location(this.latitude, this.longitude);
            }
            set
            {
                OnPropertyChanged(new PropertyChangedEventArgs("VM_Location"));
            }
        }

        public SimViewModel(NavigatorModel navigatorModel, MapAndDashboardModel mapAndDashboardModel)
        {
            this.navigatorModel = navigatorModel;
            this.mapAndDashboardModel = mapAndDashboardModel;
            // when proprety changed in the model- it will notify the VM also
            // use the map and dashboard model
            mapAndDashboardModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        public void NotifyPropertyChanged(string propName)
        {
            switch (propName)
            {
                case "HeadingDeg":
                    this.VM_HeadingDeg = MapAndDashboardModel.HeadingDeg;
                    break;
                case "VerticalSpeed": 
                    this.VM_VerticalSpeed = MapAndDashboardModel.VerticalSpeed;
                    break;
                case "GroundSpeed":
                    this.VM_GroundSpeed = MapAndDashboardModel.GroundSpeed;
                    break;
                case "AirSpeed":
                    this.VM_Airspeed = MapAndDashboardModel.Airspeed;
                    break;
                case "Altitude":
                    this.VM_Altitude = MapAndDashboardModel.Altitude;
                    break;
                case "Roll":
                    this.VM_Roll = MapAndDashboardModel.Roll;
                    break;
                case "Pitch":
                    this.VM_Pitch = MapAndDashboardModel.Pitch;
                    break;
                case "Altimeter":
                    this.VM_Altimeter = MapAndDashboardModel.Altimeter;
                    break;
                case "Latitude":
                    this.VM_Latitude = MapAndDashboardModel.Latitude;
                    //this.location.Latitude = MapAndDashboardModel.Latitude;
                    break;
                case "Longitude":
                    this.VM_Longitude = MapAndDashboardModel.Longitude;
                    //this.location.Longitude = MapAndDashboardModel.Longitude;
                    this.VM_Location = new Location (this.latitude, this.longitude);
                    Console.Write("the latitude is");
                    Console.WriteLine(this.latitude);
                    break;

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
