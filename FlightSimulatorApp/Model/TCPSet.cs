//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net.Sockets;

//namespace FlightSimulatorApp.Model
//{
//    class TCPSet
//    {
//        TcpClient tcpSet = null;
//        public TcpClient connect(string ip, int port)
//        {
//            return new TcpClient(ip, port);
//        }

//        public void disconnect()
//        {
//            tcpSet.GetStream().Close();
//            tcpSet.Close();
//            tcpSet = null;
//        }

//        public void write(string command)
//        {
//            // Translate the passed message into ASCII and store it as a Byte array.
//            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);

//            // Get a client stream for reading and writing.
//            NetworkStream stream = tcpSet.GetStream();

//            // Send the message to the connected TcpServer. 
//            stream.Write(data, 0, data.Length);
//        }

//        // from stack overflow
//        public string read()
//        {
//            if (tcpSet != null)
//            {
//                NetworkStream ns = tcpSet.GetStream();
//                if (tcpSet.ReceiveBufferSize > 0)
//                {
//                    byte[] bytes = new byte[tcpSet.ReceiveBufferSize];
//                    ns.Read(bytes, 0, tcpSet.ReceiveBufferSize);
//                    string a =Encoding.ASCII.GetString(bytes);
//                    return Encoding.ASCII.GetString(bytes); //the message incoming
//                }
//            }
//            return "ERR";
//        }
//    }
//}
