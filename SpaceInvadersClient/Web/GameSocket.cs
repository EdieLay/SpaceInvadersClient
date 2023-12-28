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

        public GameSocket()
        {
            ServerEndPoint = new IPEndPoint(serverIP, 14700);
            GameEndPoint = ServerEndPoint;
            TryToConnectTcp();
        }

        private void TryToConnectTcp()
        {
            try
            {
                TcpSocket.Connect(ServerEndPoint);
            }
            catch (SocketException) 
            {
                Thread.Sleep(100);
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
            TcpSocket.Receive(packet);
            return packet;
        }

        public void CloseTcpSocket()
        { 
            TcpSocket.Shutdown(SocketShutdown.Both);
            TcpSocket.Close(); 
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
            while (true)
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
