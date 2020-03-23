using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace FlightSimulatorApp.Model
{
    class NavigatorModel : INotifyPropertyChanged
    {
        private double throttle;
        private double elavetor;
        private double rudder;
        private double aileron;

        public event PropertyChangedEventHandler PropertyChanged;

        public double Elavetor
        {
            get { return this.elavetor; }
            set
            {
                this.elavetor = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs();
                e.Name = "elavetor";
                e.Path = "/controls/flight/elevator";
                NotifyPropertyChanged(e);
            }
        }
        public double Throttle
        {
            get { return this.throttle; }
            set
            {
                this.throttle = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs();
                e.Name = "throttle";
                e.Path = "/controls/engines/engine/throttle";
                NotifyPropertyChanged(e);
            }
        }
        public double Aileron
        {
            get { return this.aileron; }
            set
            {
                this.aileron = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs();
                e.Name = "aileron";
                e.Path = "/controls/flight/aileron";
                NotifyPropertyChanged(e);
            }
        }
        public double Rudder
        {
            get { return this.rudder; }
            set
            {
                this.rudder = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs();
                e.Name = "rudder";
                e.Path = "/controls/flight/rudder";
                NotifyPropertyChanged(e);
            }
        }

        public void NotifyPropertyChanged(PropertyChangedEventArgs propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, propName);
        }


        TcpGetSet tcpClient;
        volatile Boolean stop;

        public NavigatorModel(TcpGetSet tcpClient)
        {
            this.tcpClient = tcpClient;
            PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
            {
                Model.PropertyChangedEventArgs e1 = Model.PropertyChangedEventArgs.converPropertyChangedEventArgs(e);
                // set the property in the simulator
                tcpClient.write("set" + e1.Path + e1.Val + "\n");

            };
            stop = false;
        }

        //public NavigatorModel(TCPSet tcpCLient)
        //{
        //    this.tcpClient = tcpCLient;
        //    PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
        //    {
        //        Model.PropertyChangedEventArgs e1 = Model.PropertyChangedEventArgs.converPropertyChangedEventArgs(e);
        //        // set the property in the simulator
        //        tcpCLient.write("set" + e1.Path + e1.Val + "\n");

        //    };
        //    stop = false;
        //}

        public void connect(string ip, int port)
        {
            tcpClient.connect(ip, port);
        }
        public void disconnect()
        {
            stop = true;
            tcpClient.disconnect();
        }

    }
}



//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace FlightSimulatorApp.Model
//{
//    class NavigatorModel : INotifyPropertyChanged
//    {
//        private double throttle;
//        private double elavetor;
//        private double rudder;
//        private double aileron;

//        public event PropertyChangedEventHandler PropertyChanged;

//        public double Elavetor
//        {
//            get { return this.elavetor; }
//            set
//            {
//                this.elavetor = value;
//                PropertyChangedEventArgs e = new PropertyChangedEventArgs();
//                e.Name = "elavetor";
//                e.Path = "/controls/flight/elevator";
//                NotifyPropertyChanged(e);
//            }
//        }
//        public double Throttle
//        {
//            get { return this.throttle; }
//            set
//            {
//                this.throttle = value;
//                PropertyChangedEventArgs e = new PropertyChangedEventArgs();
//                e.Name = "throttle";
//                e.Path = "/controls/engines/engine/throttle";
//                NotifyPropertyChanged(e);
//            }
//        }
//        public double Aileron
//        {
//            get { return this.aileron; }
//            set
//            {
//                this.aileron = value;
//                PropertyChangedEventArgs e = new PropertyChangedEventArgs();
//                e.Name = "aileron";
//                e.Path = "/controls/flight/aileron";
//                NotifyPropertyChanged(e);
//            }
//        }
//        public double Rudder
//        {
//            get { return this.rudder; }
//            set
//            {
//                this.rudder = value;
//                PropertyChangedEventArgs e = new PropertyChangedEventArgs();
//                e.Name = "rudder";
//                e.Path = "/controls/flight/rudder";
//                NotifyPropertyChanged(e);
//            }
//        }

//        public void NotifyPropertyChanged(PropertyChangedEventArgs propName)
//        {
//            if (this.PropertyChanged != null)
//                this.PropertyChanged(this, propName);
//        }


//        TCPSet tcpSet;
//        volatile Boolean stop;

//        public NavigatorModel(TCPSet tcpCLient, string ip, int port)
//        {
//            this.tcpSet = tcpCLient;
//            tcpSet.connect(ip, port);
//            PropertyChanged +=  (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
//            {
//                Model.PropertyChangedEventArgs e1 = Model.PropertyChangedEventArgs.converPropertyChangedEventArgs(e);
//                // set the property in the simulator
//                tcpCLient.write("set" + e1.Path + e1.Val + "\n");

//            };
//            stop = false;
//        }

//        public NavigatorModel(TCPSet tcpCLient)
//        {
//            this.tcpSet = tcpCLient;
//            PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
//            {
//                Model.PropertyChangedEventArgs e1 = Model.PropertyChangedEventArgs.converPropertyChangedEventArgs(e);
//                // set the property in the simulator
//                tcpCLient.write("set" + e1.Path + e1.Val + "\n");

//            };
//            stop = false;
//        }

//        public void connect(string ip, int port)
//        {
//            tcpSet.connect(ip, port);
//        }
//        public void disconnect()
//        {
//            stop = true;
//            tcpSet.disconnect();
//        }

//    }
//}
