using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace FlightSimulatorApp.Model
{
    class TcpGetSet
    {
        TcpClient tcpClient = null;

        public void connect(string ip, int port)
        {
            this.tcpClient = new TcpClient(ip, port);

        }

        public void disconnect()
        {
            tcpClient.GetStream().Close();
            tcpClient.Close();
            tcpClient = null;
        }


        public void write(string command)
        {
            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);

            // Get a client stream for reading and writing.
            NetworkStream stream = tcpClient.GetStream();

            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);
        }

        // from stack overflow
        public string read()
        {
            if (tcpClient != null)
            {
                NetworkStream ns = tcpClient.GetStream();
                if (tcpClient.ReceiveBufferSize > 0)
                {
                    byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
                    ns.Read(bytes, 0, tcpClient.ReceiveBufferSize);
                    string a = Encoding.ASCII.GetString(bytes);
                    return Encoding.ASCII.GetString(bytes); //the message incoming
                }
            }
            return "ERR";
        }
    }
}
