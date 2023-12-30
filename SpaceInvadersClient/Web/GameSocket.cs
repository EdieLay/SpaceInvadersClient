using System.CodeDom;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SpaceInvadersClient
{
    public class GameSocket
    {
        EndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);

        IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        EndPoint ServerEndPoint { get; set; }
        Socket TcpSocket { get; set; } = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public GameSocket()
        {
            ServerEndPoint = new IPEndPoint(serverIP, 8791);
            TryToConnectTcp();
        }

        ~GameSocket()
        {
            TcpSocket.Close();
        }

        private void TryToConnectTcp()
        {
            try
            {
                TcpSocket.Bind(clientEP);
                TcpSocket.Connect(ServerEndPoint);
            }
            catch (SocketException) 
            {
                TryToConnectTcp();
            }
        }

        public void SendTcpPacket(byte[] packet)
        {
            try
            {
                TcpSocket.Send(packet);
            }
            catch (SocketException)
            {
                Application.Exit();
                System.Diagnostics.Process.Start(Application.ExecutablePath);
            }
        }

        public byte[] ReceiveTcpPacket()
        {
            byte[] packet = new byte[1024];
            int bytesNumber = 0;
            int RBnumber1 = 0;

            try {
                RBnumber1 = TcpSocket.Receive(packet);
            }
            catch (SocketException) {
                Application.Exit();
                System.Diagnostics.Process.Start(Application.ExecutablePath);
            }

            switch ((int)packet[0])
            {
                case (int)PacketOpcode.GameObjectsInfo:
                    bytesNumber = 19;
                    break;
                case (int)PacketOpcode.NewScore:
                    bytesNumber = 5;
                    break;
                case (int)PacketOpcode.PlayerDeath:
                    bytesNumber = 1; 
                    break;
            }

            byte[] packet2 = new byte[1024];
            int RBnumber2 = 0;
            int receiveBytesNumber = RBnumber1;
            while (receiveBytesNumber < bytesNumber)
            {
                try
                {
                    RBnumber2 = TcpSocket.Receive(packet);
                }
                catch (SocketException)
                {
                    Application.Exit();
                    System.Diagnostics.Process.Start(Application.ExecutablePath);
                }
                receiveBytesNumber += RBnumber2;
                Array.Copy(packet2, 0, packet, RBnumber1, receiveBytesNumber - RBnumber1);
            }

            if ((int)packet[0] == (int)PacketOpcode.GameObjectsInfo)
            {
                bytesNumber = 19 + (int)packet[19] * 5;
            }

            RBnumber1 = receiveBytesNumber;
            RBnumber2 = 0;
            while (receiveBytesNumber < bytesNumber)
            {
                try
                {
                    RBnumber2 = TcpSocket.Receive(packet);
                }
                catch (SocketException)
                {
                    Application.Exit();
                    System.Diagnostics.Process.Start(Application.ExecutablePath);
                }
                receiveBytesNumber += RBnumber2;
                Array.Copy(packet2, 0, packet, RBnumber1, receiveBytesNumber - RBnumber1);
            }

            return packet;
        }

        public void CloseTcpSocket()
        {
            TcpSocket.Close();
        }
    }
}
