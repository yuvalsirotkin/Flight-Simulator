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
    class MapAndDashboardModel : INotifyPropertyChanged
    {
        TcpGetSet tcpClient = null;
        TCPSet tcpSet = null;
        private Boolean stop;

        public MapAndDashboardModel(TcpGetSet tcpGet)
        {
            this.tcpClient = tcpGet;
            start();
        }

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


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(PropertyChangedEventArgs property)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(property.PropertyName));
        }


        public void connect(string ip, int port)
        {
            tcpClient.connect(ip, port);
            stop = false;
        }
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
                    Console.WriteLine("in new thread- while true");
                    string[] splittedData;

                    PropertyChangedEventArgs eHeading = new PropertyChangedEventArgs("HeadingDeg");
                    tcpClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    if (splittedData[0] != "ERR")
                    {
                        HeadingDeg = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eHeading);
                    }

                    PropertyChangedEventArgs eVerticalSpeed = new PropertyChangedEventArgs("VerticalSpeed");
                    tcpClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "veritcalSpeed";
                    if (splittedData[0] != "ERR")
                    {
                        VerticalSpeed = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eVerticalSpeed);
                    }


                    PropertyChangedEventArgs eGroundSpeed = new PropertyChangedEventArgs("GroundSpeed");
                    tcpClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "groundSpeed";
                    if (splittedData[0] != "ERR")
                    {
                        GroundSpeed = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eGroundSpeed);
                    }

                    PropertyChangedEventArgs eAirSpeed = new PropertyChangedEventArgs("AirSpeed");
                    tcpClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "airSpeed";
                    if (splittedData[0] != "ERR")
                    {
                        Airspeed = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eAirSpeed);
                    }

                    PropertyChangedEventArgs eAltitude = new PropertyChangedEventArgs("Altitude");
                    tcpClient.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    
                    if (splittedData[0] != "ERR")
                    {
                        Altitude = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eAltitude);
                    }

                    PropertyChangedEventArgs eRoll = new PropertyChangedEventArgs("Roll");
                    tcpClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "roll";
                    if (splittedData[0] != "ERR")
                    {
                        Roll = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eRoll);
                    }

                    PropertyChangedEventArgs ePitch = new PropertyChangedEventArgs("Pitch");
                    tcpClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "pitch";
                    if (splittedData[0] != "ERR")
                    {
                        Pitch = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(ePitch);
                    }

                    PropertyChangedEventArgs eAltimeter = new PropertyChangedEventArgs("Altimeter");
                    tcpClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "altimeter";
                    if (splittedData[0] != "ERR")
                    {
                        Altimeter = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eAltimeter);
                    }

                    PropertyChangedEventArgs eLongitude = new PropertyChangedEventArgs("Longitude");
                    tcpClient.write("get /position/longitude-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "altimeter";
                    if (splittedData[0] != "ERR")
                    {
                        Longitude = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eLongitude);
                    }

                    PropertyChangedEventArgs eLatitude = new PropertyChangedEventArgs("Latitude");
                    tcpClient.write("get /position/latitude-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "altimeter";
                    if (splittedData[0] != "ERR")
                    {
                        Latitude = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eLatitude);
                    }

                    // the same for the other sensors properties
                    Thread.Sleep(250);// read the data in 4Hz

                    if (stop == true)
                    {

                        Console.WriteLine("stopped");
                    }
                }
            }).Start();
        }
    }
}
