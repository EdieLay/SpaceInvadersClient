using Microsoft.VisualBasic;
using SpaceInvadersClient.Properties;
using SpaceInvadersServer;
using SpaceInvadersServer.GameObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;

namespace SpaceInvadersClient
{
    public partial class GameForm : Form
    {
        const int TIMER_INTERVAL_MS = 60;
        GameSocket socket { get; set; } // сокет для отправки и получения игровых данных
        PacketManager packetManager { get; set; } // класс для конвертации отправляющихся и полученных данных
        BattleField battleField;
        System.Timers.Timer gameTimer { get; set; }

        Image enemyImg = new Bitmap(Resources.NoobShip);
        Image playerImg = new Bitmap(Resources.PlayerShip);
        Image bulletImg = new Bitmap(Resources.Bullet);
        Image enemyBulletImg;

        int packetNumber = 0; // номер последнего принятого пакета GameObjectsInfo

        Thread dataReceiveThread; // поток, в котором выполняется постоянное принятие данных

        public GameForm(GameSocket _socket, PacketManager _packetManager)
        {
            InitializeComponent();

            socket = _socket;
            packetManager = _packetManager;
            battleField = new BattleField();
            gameTimer = new System.Timers.Timer(TIMER_INTERVAL_MS);
            enemyBulletImg = bulletImg;
            enemyBulletImg.RotateFlip(RotateFlipType.Rotate180FlipNone);

            labelLoading.Visible = true;

            // ждем Enemies And Bullets Info по UDP 
            int packetOpcode = -1;
            while (packetOpcode != (int)PacketOpcode.GameObjectsInfo)
                packetOpcode = packetManager.ParsePacket(socket.ReceiveUdpPacket(), ref battleField, ref packetNumber);

            // инициализируем игру
            InitGame();

            dataReceiveThread = new(() => { ParallelDataReceive(); });
            dataReceiveThread.Start();
        }

        private void InitGame() // инициализация игры
        {
            // событие будет порождаться в потоке из пула потоков CLR по умолчанию
            gameTimer.Elapsed += Update; 
            gameTimer.AutoReset = true;
            gameTimer.Enabled = true;

            // ???

            labelLoading.Visible = false;
        }

        private void Update(object? sender, EventArgs e)
        {
            battleField.Update();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            Enemies enemies = battleField.Enemies;
            for (int i = 0; i < enemies.boolEnemies.Length; i++)
            {
                if (enemies.boolEnemies[i])
                    graphics.DrawImage(enemyImg, enemies.X[i], enemies.Y[i], enemies.WIDTH, enemies.HEIGHT);
            }
            List<Bullet> eBullets = battleField.EnemyBullets;
            for (int i = 0; i < eBullets.Count; i++)
                graphics.DrawImage(enemyBulletImg, eBullets[i].X, eBullets[i].Y, eBullets[i].WIDTH, eBullets[i].HEIGHT);

            graphics.DrawImage(playerImg, battleField.Player.x, battleField.Player.Y, battleField.Player.WIDTH, battleField.Player.HEIGHT);

            if (battleField.PlayerBullet != null)
                graphics.DrawImage(bulletImg, battleField.PlayerBullet.X, battleField.PlayerBullet.Y, battleField.PlayerBullet.WIDTH, battleField.PlayerBullet.HEIGHT);
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) // влево
            {
                socket.SendUdpPacket(packetManager.CreateKeyDownPacket(false));
            }
            else if (e.KeyCode == Keys.D) // вправо
            {
                socket.SendUdpPacket(packetManager.CreateKeyDownPacket(true));
            }
            else if (e.KeyCode == Keys.Space) // выстрел
            {
                socket.SendUdpPacket(packetManager.CreateShotKeyDownPacket());
            }
        }
        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) // влево
            {
                socket.SendUdpPacket(packetManager.CreateKeyUpPacket(false));
            }
            else if (e.KeyCode == Keys.D) // вправо
            {
                socket.SendUdpPacket(packetManager.CreateKeyUpPacket(true));
            }
        }

        private void ParallelDataReceive()
        {
            while (true) {
                int opcode = packetManager.ParsePacket(socket.ReceiveUdpPacket(), ref battleField, ref packetNumber);
                if (opcode == (int)PacketOpcode.PlayerDeath)
                {
                    FinishGame();
                    return;
                }
            }
        }

        private void FinishGame()
        {
            gameTimer.Stop();

            Data.AddResult(Data.PlayerName, score);
            UI.ShowGameOver(gameOverText, msg, score, enemiesLeft);
        }
    }
}
