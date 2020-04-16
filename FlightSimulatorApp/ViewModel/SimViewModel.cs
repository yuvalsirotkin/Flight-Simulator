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
    public class SimViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private MapAndDashboardModel mapAndDashboardModel;
        private NavigatorModel navigatorModel;

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
        private double throttle, elevator, rudder, aileron;

        //properties
        public double VM_Elevator
        {
            get
            {
                return this.elevator;
            }
            set
            {
                this.elevator = value;
                navigatorModel.Elevator = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VM_Elevator"));
               // this.NotifyPropertyChanged("Elevator");
            }
        }
        public double VM_Throttle
        {
            get
            {
                return this.throttle;
            }
            set
            {
                this.throttle = value;
                navigatorModel.Throttle = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VM_Throttle"));
                this.NotifyPropertyChanged("VM_Throttle");
            }
        }
        public double VM_Aileron
        {
            get
            {
                return this.aileron;
            }
            set
            {
                this.aileron = value;
                navigatorModel.Aileron = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VM_Aileron"));
              //  this.NotifyPropertyChanged("Aileron");
            }
        }
        public double VM_Rudder
        {
            get
            {
                return this.rudder;
            }
            set
            {
                this.rudder = value;
                navigatorModel.Rudder = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VM_Rudder"));
              //  this.NotifyPropertyChanged("Rudder");
            }
        }



        public double VM_HeadingDeg
        {
            get {
                return this.headingDeg;
            }
            set
            {
                if (this.headingDeg >value)
                {
                    Console.WriteLine("prob");
                }
                this.headingDeg = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VM_HeadingDeg"));
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
                if (this.verticalSpeed > value)
                {
                    Console.WriteLine("prob");
                }
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

        public string VM_LongitudeMsg
        {
            get
            {
                if (MapAndDashboardModel.Longitude >= 180 || MapAndDashboardModel.Longitude <= -180)
                {
                    return "Invalid Coordinate in Longitude";
                }
                return " ";
            }

            set
            {
                OnPropertyChanged(new PropertyChangedEventArgs("VM_LongitudeMsg"));
            }
        }

        public string VM_LatitudeMsg
        {
            get
            {
                if (MapAndDashboardModel.Latitude >= 90 || MapAndDashboardModel.Latitude <= -90)
                {
                    return "Invalid Coordinate in Latitude";
                }
                return " ";
            }

            set
            {
                OnPropertyChanged(new PropertyChangedEventArgs("VM_LatitudeMsg"));
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
                case "Rudder":
                    this.VM_Rudder = MapAndDashboardModel.Rudder;
                    break;
                case "Elevator":
                    this.VM_Elevator = MapAndDashboardModel.Elevator;
                    break;
                case "Throttle":
                    this.VM_Throttle = MapAndDashboardModel.Throttle;
                    break;
                case "Aileron":
                    this.VM_Aileron = MapAndDashboardModel.Aileron;
                    break;
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
                    this.VM_LatitudeMsg = "";
                    //this.location.Latitude = MapAndDashboardModel.Latitude;
                    break;
                case "Longitude":
                    this.VM_Longitude = MapAndDashboardModel.Longitude;
                    //this.location.Longitude = MapAndDashboardModel.Longitude;
                    this.VM_Location = new Location (this.latitude, this.longitude);
                    this.VM_LongitudeMsg = "";
                    break;
                case "serverProblem":
                    this.VM_ServerMsg = "";
                    break;
                case "centerProblem":
                    this.VM_CenterMsg = "";
                    break;
            }
        }


        private bool firstTimeForServer = true;
        public string VM_ServerMsg
        {
            get
            {
                if (firstTimeForServer)
                {
                    firstTimeForServer = false;
                    return "";
                }
                return "there is a problem with the server";
                
            }

            set
            {
                OnPropertyChanged(new PropertyChangedEventArgs("VM_ServerMsg"));
            }
        }

        private bool firstTimeForCenter = true;
        public string VM_CenterMsg
        {
            get
            {
                if (firstTimeForCenter)
                {
                    firstTimeForCenter = false;
                    return "";
                }
                return "wasn't able to center the map";
            }

            set
            {
                OnPropertyChanged(new PropertyChangedEventArgs("VM_CenterMsg"));
            }
        }
    }
}
