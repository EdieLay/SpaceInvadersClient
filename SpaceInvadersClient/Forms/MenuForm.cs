using System.Net.Sockets;

namespace SpaceInvadersClient
{
    enum PacketOpcode
    {
        PressPlay = 0, // Нажата кнопка играть : к-с
        GameObjectsInfo = 1, // Инфа об игроке, пацанах и пулях : с-к
        KeyDown = 2, // Кнопка нажата (KeyDown) : к-с
        KeyUp = 3, // Кнопка отжата (KeyUp) : к-с
        ShotKeyDown = 4, // Кнопка выстрела (KeyDown) : к-с
        NewScore = 5, // Новый счёт при попадании : с-к
        PlayerDeath = 6, // Смерть игрока (конец игры) : с-к
    }

    public partial class MenuForm : Form
    {
        GameSocket socket { get; set; } // сокет для отправки и получения игровых данных
        PacketManager packetManager { get; set; } = new(); // класс для конвертации отправляющихся и полученных данных

        public MenuForm()
        {
            InitializeComponent();
            labelLoading.Show();
            buttonPlay.Hide();
            buttonResults.Hide();

            socket = new GameSocket();

            labelLoading.Hide();
            buttonPlay.Show();
            buttonResults.Show();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            // send Press Play
            socket.SendTcpPacket(packetManager.CreatePressPlayPacket());

            GameForm gameForm = new(socket);
            gameForm.Show();
            this.Hide();
        }

        private void buttonResults_Click(object sender, EventArgs e)
        {
            LeaderboardForm leaderboardForm = new(socket, this);
            leaderboardForm.Show();
            this.Hide();
        }
    }
}
