﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    interface ITCP
    {
        void connect(string ip, int port);
        void disconnect();

    }
}
