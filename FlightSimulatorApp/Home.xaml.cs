﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private string ip = "";
        private int port = 0;
        public Home()
        {
            InitializeComponent();
            DataContext = this;
        }
 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ip == "")
            {
                ip = "127.0.0.1";
            }
            if (port == 0)
            {
                port = 5402;
            }
            SimulatorView simulatorView = new SimulatorView(this.ip, this.port);
            this.NavigationService.Navigate(simulatorView);
        }
        public string ServerIP
        {
            get { return this.ip; }
            set { this.ip = value; }
        }
        public int ServerPort
        {
            get { return this.port; }
            set { this.port = value; }
        }
    }
}
