using System.Net;
using System.Net.Sockets;

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
            TcpSocket.Close();
        }
    }
}
