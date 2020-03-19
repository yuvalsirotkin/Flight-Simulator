using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;

namespace FlightSimulatorApp.Model
{
    class MapAndDashboardModel : INotifyPropertyChanged
    {
        public MapAndDashboardModel(TCPGet tcpGet)
        {
            this.tcpGet = tcpGet;
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
                    PropertyChangedEventArgs e = new PropertyChangedEventArgs();
                    tcpGet.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    string headingDeg = tcpGet.read();
                    e.Name = "headingDeg";
                    e.Val = Double.Parse(headingDeg);
                    NotifyPropertyChanged(e);

                    tcpGet.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    string veritcalSpeed = tcpGet.read();
                    e.Name = "veritcalSpeed";
                    e.Val = Double.Parse(veritcalSpeed);
                    NotifyPropertyChanged(e);

                    tcpGet.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    string groundSpeed = tcpGet.read();
                    e.Name = "groundSpeed";
                    e.Val = Double.Parse(groundSpeed);
                    NotifyPropertyChanged(e);

                    tcpGet.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    string airSpeed = tcpGet.read();
                    e.Name = "airSpeed";
                    e.Val = Double.Parse(airSpeed);
                    NotifyPropertyChanged(e);

                    tcpGet.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    string altitude = tcpGet.read();
                    e.Name = "altitude";
                    e.Val = Double.Parse(altitude);
                    NotifyPropertyChanged(e);

                    tcpGet.write("get  /instrumentation/attitude-indicator/internal-roll-deg\n");
                    string roll = tcpGet.read();
                    e.Name = "roll";
                    e.Val = Double.Parse(roll);
                    NotifyPropertyChanged(e);

                    tcpGet.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    string pitch = tcpGet.read();
                    e.Name = "pitch";
                    e.Val = Double.Parse(pitch);
                    NotifyPropertyChanged(e);

                    tcpGet.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    string altimeter = tcpGet.read();
                    e.Name = "altimeter";
                    e.Val = Double.Parse(altimeter);
                    NotifyPropertyChanged(e);

                    // the same for the other sensors properties
                    Thread.Sleep(250);// read the data in 4Hz
                }
            }).Start();
        }
    }
}
