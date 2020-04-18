using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;
//using System.ComponentModel;

namespace FlightSimulatorApp.Model
{
    public class MapAndDashboardModel : INotifyPropertyChanged
    {
        public TcpGetSet tcpClient = null;
        private Mutex mut;
        private Boolean stop;

        public MapAndDashboardModel(TcpGetSet tcpGet, Mutex mut)
        {
            this.mut = mut;
            this.tcpClient = tcpGet;
            start();
        }

        //Propreties
        public static double HeadingDeg { get; set; }
        public static double VerticalSpeed { get; set; }
        public static double GroundSpeed { get; set; }
        public static double Airspeed { get; set; }
        public static double Altitude { get; set; }
        public static double Roll { get; set; }
        public static double Pitch { get; set; }
        public static double Altimeter { get; set; }
        public static double Longitude { get; set; }
        public static double Latitude { get; set; }

        public static double Rudder { get; set; }
        public static double Elevator { get; set; }
        public static double Throttle { get; set; }
        public static double Aileron { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(PropertyChangedEventArgs property)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(property.PropertyName));
        }

        //Connect to the server.
        public void connect(string ip, int port)
        {
            tcpClient.connect(ip, port);
            stop = false;
        }

        //Disconnect from the server.
        public void disconnect()
        {
            stop = true;
            tcpClient.disconnect();
        }
    
        public void start()
        {
            new Thread(delegate () {  
                while (!stop)
                {
                    string[] splittedData;

                    //read all the data for the map and dashboard
                    PropertyChangedEventArgs eHeading = new PropertyChangedEventArgs("HeadingDeg");
                    mut.WaitOne();
                    tcpClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    mut.ReleaseMutex();
                    if (splittedData[0] != "ERR")
                    {
                        HeadingDeg = Double.Parse(splittedData[0]);
                    }
                    else
                    {
                        eHeading = new PropertyChangedEventArgs("serverProblem");
                    }
                    NotifyPropertyChanged(eHeading);


                    PropertyChangedEventArgs eVerticalSpeed = new PropertyChangedEventArgs("VerticalSpeed");
                    mut.WaitOne();
                    tcpClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    mut.ReleaseMutex();
                    if (splittedData[0] != "ERR")
                    {
                        VerticalSpeed = Double.Parse(splittedData[0]);
                    }
                    else
                    {
                        eVerticalSpeed = new PropertyChangedEventArgs("serverProblem");
                    }
                    NotifyPropertyChanged(eVerticalSpeed);


                    PropertyChangedEventArgs eGroundSpeed = new PropertyChangedEventArgs("GroundSpeed");
                    mut.WaitOne();
                    tcpClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    mut.ReleaseMutex();
                    if (splittedData[0] != "ERR")
                    {
                        GroundSpeed = Double.Parse(splittedData[0]);  
                    }
                    else
                    {
                        eGroundSpeed = new PropertyChangedEventArgs("serverProblem");  
                    }
                    NotifyPropertyChanged(eGroundSpeed);

                    PropertyChangedEventArgs eAirSpeed = new PropertyChangedEventArgs("AirSpeed");
                    mut.WaitOne();
                    tcpClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    mut.ReleaseMutex();
                    if (splittedData[0] != "ERR")
                    {
                        Airspeed = Double.Parse(splittedData[0]);
                    }
                    else
                    {
                        eAirSpeed = new PropertyChangedEventArgs("serverProblem");
                    }
                    NotifyPropertyChanged(eAirSpeed);


                    PropertyChangedEventArgs eAltitude = new PropertyChangedEventArgs("Altitude");
                    mut.WaitOne();
                    tcpClient.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    mut.ReleaseMutex();
                    if (splittedData[0] != "ERR")
                    {
                        Altitude = Double.Parse(splittedData[0]);
                    }
                    else
                    {
                        eAltitude = new PropertyChangedEventArgs("serverProblem");
                    }
                    NotifyPropertyChanged(eAltitude);

                    PropertyChangedEventArgs eRoll = new PropertyChangedEventArgs("Roll");
                    mut.WaitOne();
                    tcpClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    mut.ReleaseMutex();
                    if (splittedData[0] != "ERR")
                    {
                        Roll = Double.Parse(splittedData[0]);
                    }
                    else
                    {
                        eRoll = new PropertyChangedEventArgs("serverProblem");
                    }
                    NotifyPropertyChanged(eRoll);

                    PropertyChangedEventArgs ePitch = new PropertyChangedEventArgs("Pitch");
                    mut.WaitOne();
                    tcpClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    mut.ReleaseMutex();
                    if (splittedData[0] != "ERR")
                    {
                        Pitch = Double.Parse(splittedData[0]);
                    }
                    else
                    {
                        ePitch = new PropertyChangedEventArgs("serverProblem");
                    }
                    NotifyPropertyChanged(ePitch);

                    PropertyChangedEventArgs eAltimeter = new PropertyChangedEventArgs("Altimeter");
                    mut.WaitOne();
                    tcpClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    mut.ReleaseMutex();
                    if (splittedData[0] != "ERR")
                    {
                        Altimeter = Double.Parse(splittedData[0]);
                    }
                    else
                    {
                        eAltimeter = new PropertyChangedEventArgs("serverProblem");
                    }
                    NotifyPropertyChanged(eAltimeter);

                    PropertyChangedEventArgs eLongitude = new PropertyChangedEventArgs("Longitude");
                    mut.WaitOne();
                    tcpClient.write("get /position/longitude-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    mut.ReleaseMutex();
                    if (splittedData[0] != "ERR")
                    {
                        Longitude = Double.Parse(splittedData[0]);
                    }
                    else
                    {
                        eLongitude = new PropertyChangedEventArgs("serverProblem");
                    }
                    NotifyPropertyChanged(eLongitude);

                    PropertyChangedEventArgs eLatitude = new PropertyChangedEventArgs("Latitude");
                    mut.WaitOne();
                    tcpClient.write("get /position/latitude-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    mut.ReleaseMutex();
                    if (splittedData[0] != "ERR")
                    {
                        Latitude = Double.Parse(splittedData[0]);
                    }
                    else
                    {
                        eLatitude = new PropertyChangedEventArgs("serverProblem");
                    }
                    NotifyPropertyChanged(eLatitude);

                    Thread.Sleep(250);// read the data in 4Hz
                }
            }).Start();
            
        }
    }
}
