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

        public MapAndDashboardModel(TcpGetSet tcpGet)
        {
            this.tcpClient = tcpGet;

            start();
            stop = false;

        }

        //public MapAndDashboardModel(TcpGetSet tcpClient)
        //{
        //    this.tcpSet = tcpSet;
        //    start();
        //    stop = false;

        //}

        //public MapAndDashboardModel(TCPGet tcpGet)
        //{
        //    this.tcpGet = tcpGet;
        //    start();
        //    stop = false;

        //}

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(PropertyChangedEventArgs propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, propName);
        }

        
        volatile Boolean stop;

        public void connect(string ip, int port)
        {
            tcpClient.connect(ip, port);
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
                    string[] splittedData;
                    PropertyChangedEventArgs e = new PropertyChangedEventArgs();

                    tcpClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    e.Name = "headingDeg";
                    if (splittedData[0] != "ERR")
                    {
                        e.Val = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(e);
                    }
 
                    tcpClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    e.Name = "veritcalSpeed";
                    if (splittedData[0] != "ERR")
                    {
                        e.Val = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(e);
                    }

                    tcpClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    e.Name = "groundSpeed";
                    if (splittedData[0] != "ERR")
                    {
                        e.Val = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(e);
                    }

                    tcpClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    e.Name = "airSpeed";
                    if (splittedData[0] != "ERR")
                    {
                        e.Val = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(e);
                    }

                    tcpClient.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    e.Name = "altitude";
                    if (splittedData[0] != "ERR")
                    {
                        e.Val = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(e);
                    }

                    tcpClient.write("get  /instrumentation/attitude-indicator/internal-roll-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    e.Name = "roll";
                    if (splittedData[0] != "ERR")
                    {
                        e.Val = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(e);
                    }
                    tcpClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    e.Name = "pitch";
                    if (splittedData[0] != "ERR")
                    {
                        e.Val = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(e);
                    }
                    tcpClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                    e.Name = "altimeter";
                    if (splittedData[0] != "ERR")
                    {
                        e.Val = Double.Parse(splittedData[0]);
                        NotifyPropertyChanged(e);
                    }

                    // the same for the other sensors properties
                    Thread.Sleep(250);// read the data in 4Hz
                }
            }).Start();
        }
    }
}
