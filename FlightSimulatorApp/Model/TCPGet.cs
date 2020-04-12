//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net.Sockets;
//using System.Net;


//namespace FlightSimulatorApp.Model
//{
//    class TCPGet
//    {
//        TcpClient tcpGet = null;
//        TcpListener server = null;
//        Byte[] bytes = new Byte[256];
//        String data = null;

//        public TCPGet(Int32 port, string ip)
//        {
//            IPAddress localAddr = IPAddress.Parse(ip);
//            server = new TcpListener(localAddr, port);
//            server.Start();
//            // only for test
//            Console.Write("only for test: Waiting for a connection... ");
//            // not only for test!
//            TcpClient client = server.AcceptTcpClient();
//            // only for test
//            Console.WriteLine("only for test: Connected!");
//        }

//        public void connect(string ip, int port)
//        {
//            TcpClient tcpGet = new TcpClient(ip, port);
//        }

//        public void disconnect()
//        {
//            tcpGet.GetStream().Close();
//            tcpGet.Close();
//            tcpGet = null;
//        }

//        // from stack overflow
//        public string read()
//        {
//            if (tcpGet != null)
//            {
//                NetworkStream ns = tcpGet.GetStream();
//                if (tcpGet.ReceiveBufferSize > 0)
//                {
//                    byte[] bytes = new byte[tcpGet.ReceiveBufferSize];
//                    ns.Read(bytes, 0, tcpGet.ReceiveBufferSize);
//                    return Encoding.ASCII.GetString(bytes); //the message incoming
//                }
//            }
//            return "ERR";
//        }

//        // maybe we can write it in new interface of tcp
//        public void write(string command)
//        {
//            // Translate the passed message into ASCII and store it as a Byte array.
//            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);

//            // Get a client stream for reading and writing.
//            NetworkStream stream = tcpGet.GetStream();

//            // Send the message to the connected TcpServer. 
//            stream.Write(data, 0, data.Length);
//        }
//    }
//}


////namespace FlightSimulatorApp.Model
////{
////    class TCPGet 
////    {
////        TcpListener server = null;
////        Byte[] bytes = new Byte[256];
////        String data = null;

////        public TCPGet(Int32 port, string ip)
////        {
////            IPAddress localAddr = IPAddress.Parse(ip);
////            server = new TcpListener(localAddr, port);
////            server.Start();
////            // only for test
////            Console.Write("only for test: Waiting for a connection... ");
////            // not only for test!
////            TcpClient client = server.AcceptTcpClient();
////            // only for test
////            Console.WriteLine("only for test: Connected!");
////        }


////        public void connect(string ip, int port)
////        {
////            TcpClient tcpSet = new TcpClient(ip, port);
////        }



////        // from stack overflow
////        public string read()
////        {
////            if (server != null)
////            {
////                NetworkStream ns = server.GetStream();
////                if (server.ReceiveBufferSize > 0)
////                {
////                    byte[] bytes = new byte[server.ReceiveBufferSize];
////                    ns.Read(bytes, 0, server.ReceiveBufferSize);
////                    return Encoding.ASCII.GetString(bytes); //the message incoming
////                }
////            }
////            return "ERR";
////        }
////    }
////}




