using System.Net;
using System.Net.Sockets;

namespace SpaceInvadersClient
{
    public class GameSocket
    {
        IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        EndPoint ServerEndPoint { get; set; }
        EndPoint GameEndPoint { get; set; }
        Socket TcpSocket { get; set; } = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket UdpSocket { get; set; } = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        bool TcpSocketIsOpen = true;
        public bool Visible { get; set; } = true;

        public GameSocket()
        {
            ServerEndPoint = new IPEndPoint(serverIP, 8791);
            GameEndPoint = ServerEndPoint;
            TryToConnectTcp();
        }

        ~GameSocket()
        {
            Visible = false;
            UdpSocket.Close();
            TcpSocket.Close();
        }

        private void TryToConnectTcp()
        {
            try
            {
                TcpSocket.Connect(ServerEndPoint);
            }
            catch (SocketException) 
            {
                TryToConnectTcp();
            }
        }

        public void InitUdpSocket(int port)
        {
            GameEndPoint = new IPEndPoint(serverIP, port);
            UdpSocket.Connect(GameEndPoint);
        }

        public void SendTcpPacket(byte[] packet)
        {
            TcpSocket.Send(packet);
        }

        public byte[] ReceiveTcpPacket()
        {
            byte[] packet = new byte[1024];
            try
            {
                TcpSocket.Receive(packet);
            }
            catch (SocketException)
            {
                Application.Exit();
                System.Diagnostics.Process.Start(Application.ExecutablePath);
            }
            return packet;
        }

        public void CloseTcpSocket()
        { 
            TcpSocket.Shutdown(SocketShutdown.Both);
            TcpSocket.Close();
            TcpSocketIsOpen = false;
        }

        public void SendUdpPacket(byte[] packet)
        {
            Thread thread = new(() =>
            {
                UdpSocket.SendTo(packet, ServerEndPoint);
            });
            thread.Start();
        }

        public byte[] ReceiveUdpPacket()
        {
            using MemoryStream ms = new();
            while (Visible)
            {
                byte[] data = new byte[1024];
                int peek = UdpSocket.Receive(data, 0, 0, SocketFlags.Peek); // SocketFlags.Peek - возвращает кол-во доступных байт
                if (peek == 0) break;
                UdpSocket.Receive(data);
                ms.Write(data, 0, peek);
            }
            return ms.ToArray();
        }
    }
}
