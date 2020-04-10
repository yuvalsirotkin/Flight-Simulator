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

        public MapAndDashboardModel(TcpGetSet tcpGet)
        {
            this.mut = new Mutex();
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
                    mut.WaitOne();
                    Console.WriteLine("1");
                    string[] splittedData;

                    PropertyChangedEventArgs eHeading = new PropertyChangedEventArgs("HeadingDeg");
                    tcpClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    if (splittedData[0] != "ERR")
                    {
                        HeadingDeg = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eHeading);
                    }
                    Console.WriteLine("2");
                    PropertyChangedEventArgs eVerticalSpeed = new PropertyChangedEventArgs("VerticalSpeed");
                    tcpClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "veritcalSpeed";
                    if (splittedData[0] != "ERR")
                    {
                        VerticalSpeed = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eVerticalSpeed);
                    }
                    Console.WriteLine("3");

                    PropertyChangedEventArgs eGroundSpeed = new PropertyChangedEventArgs("GroundSpeed");
                    tcpClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "groundSpeed";
                    if (splittedData[0] != "ERR")
                    {
                        GroundSpeed = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eGroundSpeed);
                    }
                    Console.WriteLine("4");
                    PropertyChangedEventArgs eAirSpeed = new PropertyChangedEventArgs("AirSpeed");
                    tcpClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "airSpeed";
                    if (splittedData[0] != "ERR")
                    {
                        Airspeed = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eAirSpeed);
                    }
                    Console.WriteLine("5");
                    PropertyChangedEventArgs eAltitude = new PropertyChangedEventArgs("Altitude");
                    tcpClient.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    
                    if (splittedData[0] != "ERR")
                    {
                        Altitude = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eAltitude);
                    }
                    Console.WriteLine("6");
                    PropertyChangedEventArgs eRoll = new PropertyChangedEventArgs("Roll");
                    tcpClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "roll";
                    if (splittedData[0] != "ERR")
                    {
                        Roll = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eRoll);
                    }
                    Console.WriteLine("7");
                    PropertyChangedEventArgs ePitch = new PropertyChangedEventArgs("Pitch");
                    tcpClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "pitch";
                    if (splittedData[0] != "ERR")
                    {
                        Pitch = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(ePitch);
                    }
                    Console.WriteLine("8");
                    PropertyChangedEventArgs eAltimeter = new PropertyChangedEventArgs("Altimeter");
                    tcpClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "altimeter";
                    if (splittedData[0] != "ERR")
                    {
                        Altimeter = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eAltimeter);
                    }
                    Console.WriteLine("9");
                    PropertyChangedEventArgs eLongitude = new PropertyChangedEventArgs("Longitude");
                    tcpClient.write("get /position/longitude-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "altimeter";
                    if (splittedData[0] != "ERR")
                    {
                        Longitude = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eLongitude);
                    }
                    Console.WriteLine("10");
                    PropertyChangedEventArgs eLatitude = new PropertyChangedEventArgs("Latitude");
                    tcpClient.write("get /position/latitude-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    //e.Name = "altimeter";
                    if (splittedData[0] != "ERR")
                    {
                        Latitude = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(eLatitude);
                    }
                    Console.WriteLine("11");     
                    // the same for the other sensors properties
                    Thread.Sleep(250);// read the data in 4Hz
                    mut.ReleaseMutex();

                    if (stop == true)
                    {

                        Console.WriteLine("stopped");
                    }
                }
            }).Start();
            
        }
    }
}
