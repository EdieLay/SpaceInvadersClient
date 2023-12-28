using SpaceInvadersServer;
using SpaceInvadersServer.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersClient
{
    public class PacketManager
    {
        public PacketManager() { }

        public byte[] CreateLaunchClientPacket() // 0
        {
            return new byte[] { Convert.ToByte(PacketOpcode.LaunchClient) };
        }

        public byte[] CreatePressPlayPacket() // 2
        {
            return new byte[] { Convert.ToByte(PacketOpcode.PressPlay) };
        }

        public byte[] CreateKeyDownPacket(bool flagRight) // 4
        {
            return new byte[] { Convert.ToByte(PacketOpcode.KeyDown), Convert.ToByte(flagRight) };
        }

        public byte[] CreateKeyUpPacket(bool flagRight) // 5
        {
            return new byte[] { Convert.ToByte(PacketOpcode.KeyUp), Convert.ToByte(flagRight) };
        }

        public byte[] CreateShotKeyDownPacket() // 6
        {
            return new byte[] { Convert.ToByte(PacketOpcode.ShotKeyDown) };
        }

        public int ParsePacket(byte[] packet, ref int port)
        {
            BattleField? bf = null;
            return ParsePacket(packet, ref bf, ref port);
        }
        public int ParsePacket(byte[] packet, ref BattleField bf)
        {
            int port = -1;
            return ParsePacket(packet, ref bf, ref port);
        }
        public int ParsePacket(byte[] packet, ref BattleField bf, ref int port)
        {
            // ???
            return -1;
        }
    }
}