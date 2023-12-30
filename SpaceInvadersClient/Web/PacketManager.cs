using SpaceInvadersServer;

namespace SpaceInvadersClient
{
    public class PacketManager
    {
        public PacketManager() { }

        public byte[] CreatePressPlayPacket() // 0
        {
            return new byte[] { (byte)PacketOpcode.PressPlay };
        }

        public byte[] CreateKeyDownPacket(bool flagRight) // 2
        {
            return new byte[] { (byte)PacketOpcode.KeyDown, Convert.ToByte(flagRight) };
        }

        public byte[] CreateKeyUpPacket(bool flagRight) // 3
        {
            return new byte[] { (byte)PacketOpcode.KeyUp, Convert.ToByte(flagRight) };
        }

        public byte[] CreateShotKeyDownPacket() // 4
        {
            return new byte[] { (byte)PacketOpcode.ShotKeyDown };
        }

        public int ParsePacket(byte[] packet, ref int port)
        {
            BattleField? bf = null;
            int packetNumber = -1;
            return ParsePacket(packet, ref bf, ref port, ref packetNumber);
        }
        public int ParsePacket(byte[] packet, ref BattleField bf, ref int packetNumber)
        {
            int port = -1;
            return ParsePacket(packet, ref bf, ref port, ref packetNumber);
        }

        //PressPlay = 0, // Нажата кнопка играть : к-с
        //GameObjectsInfo = 1, // Инфа об игроке, пацанах и пулях : с-к
        //KeyDown = 2, // Кнопка нажата (KeyDown) : к-с
        //KeyUp = 3, // Кнопка отжата (KeyUp) : к-с
        //ShotKeyDown = 4, // Кнопка выстрела (KeyDown) : к-с
        //NewScore = 5, // Новый счёт при попадании : с-к
        //PlayerDeath = 6, // Смерть игрока (конец игры) : с-к
        public int ParsePacket(byte[] packet, ref BattleField bf, ref int port, ref int packetNumber)
        {
            if (packet == null || packet.Length == 0) return -1;
            switch ((int)packet[0])
            {
                case (int)PacketOpcode.GameObjectsInfo:
                    if (bf == null || packetNumber == -1 || packet.Length < 19) return -1;

                    int testPacketNumber = (packet[1] << 8) + packet[2];
                    if (testPacketNumber != packetNumber + 1) return -1;
                    packetNumber++;

                    bf.Player.x = (packet[3] << 8) + packet[4];
                    bf.Enemies.offsetX = (packet[5] << 8) + packet[6];
                    bf.Enemies.offsetY = (packet[7] << 8) + packet[8];
                    bf.Enemies.speed = (packet[9] << 8) + packet[10];
                    for (int i = 11; i <= 17; i++)
                    {
                        byte alive = packet[i];
                        for (int j = 0; j < 8; j++)
                        {
                            if (8 * (i - 11) + j < bf.Enemies.boolEnemies.Length)
                            {
                                if (1 == ((alive >> 7 - j) & 0b_0000_0001))
                                    bf.Enemies.boolEnemies[8 * (i - 11) + j] = true;
                                else
                                    bf.Enemies.boolEnemies[8 * (i - 11) + j] = false;
                            }
                        }
                    }
                    bf.Enemies.Sync();

                    int bulletsNumber = packet[18];
                    if (packet.Length < 19 + bulletsNumber * 5) return -1;
                    bf.EnemyBullets.Clear();
                    bf.PlayerBullet = new(0, 0, 0, false);
                    int ind = 19;
                    while (ind < 19 + bulletsNumber * 5)
                    {
                        int x = (packet[ind++] << 8) + packet[ind++];
                        int y = (packet[ind++] << 8) + packet[ind++];
                        int speed = packet[ind++];
                        if (speed > 0) bf.EnemyBullets.Add(new Bullet(x, y, speed, true));
                        else bf.PlayerBullet = new(x, y, speed, true);
                    }
                    return (int)PacketOpcode.GameObjectsInfo;

                case (int)PacketOpcode.NewScore:
                    if (bf == null || packetNumber == -1 || packet.Length < 5) return -1;
                    bf.Score = (packet[1] << 24) + (packet[2] << 16) + (packet[3] << 8) + packet[4];
                    return (int)PacketOpcode.NewScore;

                case (int)PacketOpcode.PlayerDeath:
                    return (int)PacketOpcode.PlayerDeath;
            }
            return -1;
        }
    }
}