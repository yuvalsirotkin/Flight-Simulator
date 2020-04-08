using System;
using System.ComponentModel;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace FlightSimulatorApp.Model
{
    public class NavigatorModel : INotifyPropertyChanged
    {
        private double throttle;
        private double elevetor;
        private double rudder;
        private double aileron;

        public event PropertyChangedEventHandler PropertyChanged;

        public double Elavetor
        {
            get { return this.elevetor; }
            set
            {
                this.elevetor = value;
                if (value > 1)
                {
                    this.elevetor = 1;
                }
                if (value < -1)
                {
                    this.elevetor = -1;
                }
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("elevetor");
                //e.Name = "elavetor";
                //e.Path = "/controls/flight/elevator";
                //e.Val = value;
                NotifyPropertyChanged(e);
            }
        }
        public double Throttle
        {
            get { return this.throttle; }
            set
            {
                this.throttle = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("throttle");
                //e.Name = "throttle";
                //e.Path = "/controls/engines/current-engine/throttle";
                //e.Val = value;
                NotifyPropertyChanged(e);
            }
        }
        public double Aileron
        {
            get { return this.aileron; }
            set
            {
                this.aileron = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("aileron");
                //e.Name = "aileron";
                //e.Path = "/controls/flight/aileron";
                //e.Val = value;
                NotifyPropertyChanged(e);
            }
        }
        public double Rudder
        {
            get { return this.rudder; }
            set
            {
                this.rudder = value;
                if (value > 1)
                {
                    this.rudder = 1;
                }
                if (value < -1)
                {
                    this.rudder = -1;
                }
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("rudder");
                //e.Name = "rudder";
                //e.Path = "/controls/flight/rudder";
                //e.Val = value;
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

        //לכי על זה

        public NavigatorModel(TcpGetSet tcpClient)
        {
            this.tcpClient = tcpClient;
            PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                string path = "";
                double val = 0;
                switch (e.PropertyName)
                {
                    case "elevetor":
                        path = "/controls/flight/elevator";
                        val = Elavetor;
                        if (val != 0)
                        {
                            Console.WriteLine("el not 0");
                        }
                        break;
                    case "throttle":
                        path = "/controls/engines/current-engine/throttle";
                        val = Throttle;
                        if (val == 1)
                        {
                            Console.WriteLine("th is 1");
                        }
                        break;
                    case "aileron":
                        path = "/controls/flight/aileron";
                        val = Aileron;
                        if (val != 0)
                        {
                            Console.WriteLine("ai not 0");
                        }
                        break;
                    case "rudder":
                        path = "/controls/flight/rudder";
                        val = Rudder;
                        if (val != 0)
                        {
                            Console.WriteLine("ru not 0");
                        }
                        break;


                }
                // set the property in the simulator
                tcpClient.write("set " + path + " " + val + "\n");
                string[] splittedData = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                Console.WriteLine(splittedData[0]);


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
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Net.Sockets;

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


//        TcpGetSet tcpClient;
//        volatile Boolean stop;

//        public NavigatorModel(TcpGetSet tcpClient)
//        {
//            this.tcpClient = tcpClient;
//            PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
//            {
//                Model.PropertyChangedEventArgs e1 = Model.PropertyChangedEventArgs.converPropertyChangedEventArgs(e);
//                // set the property in the simulator
//                tcpClient.write("set" + e1.Path + e1.Val + "\n");

//            };
//            stop = false;
//        }

//        //public NavigatorModel(TCPSet tcpCLient)
//        //{
//        //    this.tcpClient = tcpCLient;
//        //    PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
//        //    {
//        //        Model.PropertyChangedEventArgs e1 = Model.PropertyChangedEventArgs.converPropertyChangedEventArgs(e);
//        //        // set the property in the simulator
//        //        tcpCLient.write("set" + e1.Path + e1.Val + "\n");

//        //    };
//        //    stop = false;
//        //}

//        public void connect(string ip, int port)
//        {
//            tcpClient.connect(ip, port);
//        }
//        public void disconnect()
//        {
//            stop = true;
//            tcpClient.disconnect();
//        }

//    }
//}


