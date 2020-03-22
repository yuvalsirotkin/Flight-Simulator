using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;

namespace FlightSimulatorApp.Model
{
    class MapAndDashboardModel : INotifyPropertyChanged
    {
        public MapAndDashboardModel(TCPGet tcpGet, string ip, int port)
        {
            this.tcpGet = tcpGet;
            tcpGet.connect(ip, port);
            stop = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(PropertyChangedEventArgs propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, propName);
        }

        TCPGet tcpGet;
        volatile Boolean stop;

        public void connect(string ip, int port)
        {
            tcpGet.connect(ip, port);
        }
        public void disconnect()
        {
            stop = true;
            tcpGet.disconnect();
        }


        public void start()
        {
            new Thread(delegate () {
                while (!stop)
                {
                    string[] splittedData;
                    PropertyChangedEventArgs e = new PropertyChangedEventArgs();

                    tcpGet.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpGet.read(), "\n");
                    e.Name = "headingDeg";
                    e.Val = Double.Parse(splittedData[0]);
                    NotifyPropertyChanged(e);

                    tcpGet.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpGet.read(), "\n");
                    e.Name = "veritcalSpeed";
                    e.Val = Double.Parse(splittedData[0]);
                    NotifyPropertyChanged(e);

                    tcpGet.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpGet.read(), "\n");
                    e.Name = "groundSpeed";
                    e.Val = Double.Parse(splittedData[0]);
                    NotifyPropertyChanged(e);

                    tcpGet.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpGet.read(), "\n");
                    e.Name = "airSpeed";
                    e.Val = Double.Parse(splittedData[0]);
                    NotifyPropertyChanged(e);

                    tcpGet.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpGet.read(), "\n");
                    e.Name = "altitude";
                    e.Val = Double.Parse(splittedData[0]);
                    NotifyPropertyChanged(e);

                    tcpGet.write("get  /instrumentation/attitude-indicator/internal-roll-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpGet.read(), "\n");
                    e.Name = "roll";
                    e.Val = Double.Parse(splittedData[0]);
                    NotifyPropertyChanged(e);

                    tcpGet.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpGet.read(), "\n");
                    e.Name = "pitch";
                    e.Val = Double.Parse(splittedData[0]);
                    NotifyPropertyChanged(e);

                    tcpGet.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpGet.read(), "\n");
                    e.Name = "altimeter";
                    e.Val = Double.Parse(splittedData[0]);
                    NotifyPropertyChanged(e);

                    // the same for the other sensors properties
                    Thread.Sleep(250);// read the data in 4Hz
                }
            }).Start();
        }
    }
}
