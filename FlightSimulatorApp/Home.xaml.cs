using System;
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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;



namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private string ip = "";
        private string port = "";
        private int portInInt = 0;

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public Home()
        {
            InitializeComponent();
            DataContext = this;
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var hwnd = new WindowInteropHelper(this).Handle;
        //    SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        //}

        private void DefaultCommand(object sender, RoutedEventArgs e)
        {
            this.ip = "127.0.0.1";
            this.port = "5402";
            IP.Text = this.ip;
            Port.Text = this.port;
        }

        private void SetCommand(object sender, RoutedEventArgs e)
        {
            this.ip = ServerIP;
            this.port = ServerPort;
        }
        private void FlyCommand(object sender, RoutedEventArgs e)
        {
            if (ip == "" && port == "")
            {
                string message = String.Format("You must enter IP & Port adress");
                ErrMsg.Text = message;
            }

            else if (ip == "")
            {
                string message = String.Format("You must enter IP adress");
                ErrMsg.Text = message;
                //MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (port == "")
            {
                string message = String.Format("You must enter Port adress");
                ErrMsg.Text = message;
                //MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    portInInt = int.Parse(port);
                    SimulatorView simulatorView = new SimulatorView(this.ip, portInInt);
                    this.NavigationService.Navigate(simulatorView);
                }
                catch
                {
                    string message = String.Format("Not able to connet, Something went wrong");
                    ErrMsg.Text = message;
                }
            }
        }
        public string ServerIP
        {
            get { return this.ip; }
            set { this.ip = value; }
        }
        public string ServerPort
        {
            get { return this.port; }
            set { this.port = value; }
        }
    }
}
