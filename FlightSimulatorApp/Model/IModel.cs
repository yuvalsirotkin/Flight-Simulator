using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    interface IModel : INotifyPropertyChanged
    {

        // connection to the simulator
        void connect(string ip, int port);
        void disconnect();

        // sensors properties
        double Throttle { set; get; }
        double Aileron { set; get; }
        double Elavetor { set; get; }
        double Rudder { set; get; }

    }
}
