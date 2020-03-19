using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace FlightSimulatorApp.Model
{
    class TCPSet : ITCP
    {
        TcpClient tcpSet = null;
        public void connect(string ip, int port)
        {
            TcpClient tcpSet = new TcpClient(ip, port);
        }

        public void disconnect()
        {
            tcpSet.GetStream().Close();
            tcpSet.Close();
            tcpSet = null;
        }

        public void write(string command)
        {
            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);

            // Get a client stream for reading and writing.
            NetworkStream stream = tcpSet.GetStream();

            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);
        }
    }
}
