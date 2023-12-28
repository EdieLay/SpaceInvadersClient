using SpaceInvadersServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvadersClient
{
    enum PacketOpcode
    {
        LaunchClient = 0, // Новый клиент запустил игру : к-с
        OpenNewSocket = 1, // Открытие нового сокета : с-к
        PressPlay = 2, // Нажата кнопка играть : к-с
        GameObjectsInfo = 3, // Инфа об игроке, пацанах и пулях : с-к
        KeyDown = 4, // Кнопка нажата (KeyDown) : к-с
        KeyUp = 5, // Кнопка отжата (KeyUp) : к-с
        ShotKeyDown = 6, // Кнопка выстрела (KeyDown) : к-с
        NewScore = 7, // Новый счёт при попадании : с-к
        PlayerDeath = 8, // Смерть игрока (конец игры) : с-к
    }

    public partial class MenuForm : Form
    {
        GameSocket socket { get; set; } // сокет для отправки и получения игровых данных
        PacketManager packetManager { get; set; } // класс для конвертации отправляющихся и полученных данных
        DataManager dataManager { get; set; }

        public MenuForm()
        {
            InitializeComponent();
            labelLoading.Show();
            buttonPlay.Hide();
            buttonResults.Hide();

            packetManager = new PacketManager();

            Shown += new EventHandler(StartConnectionToServer);
        }

        private void StartConnectionToServer(object? sender, EventArgs e)
        {
            int port = 0;
            Thread thread = new(() => {
                socket = new GameSocket();
                // send Launch Client
                socket.SendTcpPacket(packetManager.CreateLaunchClientPacket());
                // ждем OpenNewSocket
                int packetOpcodeNumber = -1;
                while (packetOpcodeNumber != (int)PacketOpcode.OpenNewSocket)
                {
                    packetOpcodeNumber = packetManager.ParsePacket(socket.ReceiveTcpPacket(), ref port);
                }
            });
            thread.Start();
            thread.Join();

            socket.InitUdpSocket(port);

            labelLoading.Hide();
            buttonPlay.Show();
            buttonResults.Show();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            // send Press Play
            socket.SendTcpPacket(packetManager.CreatePressPlayPacket());

            GameForm gameForm = new(socket, packetManager, dataManager);
            gameForm.Show();
            this.Hide();
        }

        private void buttonResults_Click(object sender, EventArgs e)
        {
            LeaderboardForm leaderboardForm = new(socket, packetManager, dataManager, this);
            leaderboardForm.Show();
            this.Hide();
        }
    }
}
