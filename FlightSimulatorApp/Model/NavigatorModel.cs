﻿using System;
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

        //Properties.
        public double Elavetor
        {
            get { return this.elevetor; }
            set
            {
                this.elevetor = value;
                //Check if the value is valid.
                if (value > 1)
                {
                    this.elevetor = 1;
                }
                if (value < -1)
                {
                    this.elevetor = -1;
                }
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("elevetor");
                mut.WaitOne();
                NotifyPropertyChanged(e);
                mut.ReleaseMutex();
            }
        }
        public double Throttle
        {
            get { return this.throttle; }
            set
            {
                this.throttle = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("throttle");
                mut.WaitOne();
                NotifyPropertyChanged(e);
                mut.ReleaseMutex();
            }
        }
        public double Aileron
        {
            get { return this.aileron; }
            set
            {
                this.aileron = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("aileron");
                mut.WaitOne();
                NotifyPropertyChanged(e);
                mut.ReleaseMutex();
            }
        }
        public double Rudder
        {
            get { return this.rudder; }
            set
            {
                this.rudder = value;
                //Check if the value is valid.
                if (value > 1)
                {
                    this.rudder = 1;
                }
                if (value < -1)
                {
                    this.rudder = -1;
                }
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("rudder");
                mut.WaitOne();
                NotifyPropertyChanged(e);
                mut.ReleaseMutex();
            }
        }
        
        public void NotifyPropertyChanged(PropertyChangedEventArgs propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, propName);
        }

        private static readonly object locker = new object();

        TcpGetSet tcpClient;
        Mutex mut = new Mutex();
        public NavigatorModel(TcpGetSet tcpClient, Mutex mut)
        {
            this.mut = mut;
            this.tcpClient = tcpClient;
            //Set the propertyChanged function.
            PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                string path = "";
                double val = 0;
                switch (e.PropertyName)
                {
                    case "elevetor":
                        path = "/controls/flight/elevator";
                        val = Elavetor;
                        break;
                    case "throttle":
                        path = "/controls/engines/current-engine/throttle";
                        val = Throttle;
                        break;
                    case "aileron":
                        path = "/controls/flight/aileron";
                        val = Aileron;
                        break;
                    case "rudder":
                        path = "/controls/flight/rudder";
                        val = Rudder;
                        break;


                }
                // set the property in the 
                mut.WaitOne();
                tcpClient.write("set " + path + " " + val + "\n");
                string[] splittedData1 = System.Text.RegularExpressions.Regex.Split(tcpClient.read(), "\n");
                mut.ReleaseMutex();


            };
        }
    }
}


