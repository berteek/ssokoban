using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SSokoban.Utils
{
    public static class Network
    {
        private static string remoteAddress;
        private static int remotePort;

        private static IPEndPoint remoteEndPoint;

        private static UdpClient sender;
        private static UdpClient receiver;

        private static Thread connectionThread;
        private static Thread receiveThread;
        private static bool isReceiving = true;

        public static int LocalPort { get; set; } = 55555;

        public static bool isConnected = false;
        public static bool isConnecting = false;

        public static void Connect(string remoteAddress, int remotePort, Action onSent, Action onConnected)
        {
            if (isConnected || isConnecting)
            {
                Close();
            }

            try { remoteEndPoint = IPEndPoint.Parse(remoteAddress); } catch { return; }

            bool isConnectingToItself = false;
            GetLocalIPs().ForEach((ip) =>
            {
                if (remoteAddress.Equals(ip) && remotePort == LocalPort)
                    isConnectingToItself = true;        
            });
            if (isConnectingToItself)
                return;

            sender = new UdpClient();
            receiver = new UdpClient(LocalPort);

            Network.remoteAddress = remoteAddress;
            Network.remotePort = remotePort;

            Send("connect");

            connectionThread = new Thread(new ThreadStart(() =>
            {
                onSent();
                isConnecting = true;
                byte[] data;
                try
                {
                    data = receiver.Receive(ref remoteEndPoint);
                }
                catch
                {
                    data = new byte[0];
                }
                string message = Encoding.UTF8.GetString(data);
                if (message.Equals("connect"))
                {
                    Send("connect");

                    isConnected = true;
                    isConnecting = false;
                    onConnected();
                }

                receiveThread = new Thread(new ThreadStart(Receive));
                receiveThread.Start();
            }));

            connectionThread.Start();
        }

        private static List<string> GetLocalIPs()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            List<string> ips = new List<string>();

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ips.Add(ip.ToString());
                }
            }
            return ips;
        }

        private static void Receive()
        {
            while (isReceiving)
            {
                byte[] data = receiver.Receive(ref remoteEndPoint);
                string message = Encoding.UTF8.GetString(data);
                Commander.Process(message);
            }
        }

        public static void Send(string message)
        {
            if (sender != null)
            {
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    sender.Send(data, data.Length, remoteAddress, remotePort);
                }
                catch
                { }
            }
        }

        public static void Close()
        {
            Send("disconnect");
            isReceiving = false;
            isConnected = false;
            isConnecting = false;
            if (receiver != null)
                receiver.Close();
            if (sender != null)
                sender.Close();
        }
    }
}