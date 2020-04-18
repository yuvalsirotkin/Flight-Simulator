using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.ComponentModel;
using System.Threading;

namespace FlightSimulatorApp.Model
{
    public class TcpGetSet : INotifyPropertyChanged
    {
        TcpClient tcpClient = null;
       
        //Connect to the server
        public void connect(string ip, int port)
        {
            this.tcpClient = new TcpClient(ip, port);
        }

        //Disconnect from the server
        public void disconnect()
        {
            try
            {
                tcpClient.GetStream().Close();
                tcpClient.Close();
                tcpClient = null;
            }
            catch (Exception e)
            {
                System.Environment.Exit(0);
            }
        }

        //Write to the simulator
        public void write(string command)
        {
            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
            try { 

                // Get a client stream for reading and writing.
                NetworkStream stream = tcpClient.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                PropertyChangedEventArgs eDisconnect = new PropertyChangedEventArgs("Disconnect");
                NotifyPropertyChanged(eDisconnect);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(PropertyChangedEventArgs property)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(property.PropertyName));
        }

 
        // from stack overflow - read the data from the simulator
        public string read()
        {
           
            if (tcpClient != null)
            {
                try
                {
                    NetworkStream ns = tcpClient.GetStream();
                    if (tcpClient.ReceiveBufferSize > 0)
                    {
                        byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
                        ns.ReadTimeout = 10000;
                        ns.Read(bytes, 0, tcpClient.ReceiveBufferSize);
                        string a = Encoding.ASCII.GetString(bytes);
                        return Encoding.ASCII.GetString(bytes); //the message incoming
                    }
                }
                catch (Exception e)
                {
                   
                        PropertyChangedEventArgs eServerPr = new PropertyChangedEventArgs("serverProblem");
                        NotifyPropertyChanged(eServerPr);
                    
                }
            }
            return "ERR";
        }
    }
}



