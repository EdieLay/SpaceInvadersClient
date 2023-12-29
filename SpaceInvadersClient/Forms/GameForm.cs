using SpaceInvadersClient.Properties;
using SpaceInvadersServer;

namespace SpaceInvadersClient
{
    public partial class GameForm : Form
    {
        const int TIMER_INTERVAL_MS = 30;
        GameSocket socket { get; set; } // сокет для отправки и получения игровых данных
        PacketManager packetManager { get; set; } = new(); // класс для конвертации отправляющихся и полученных данных
        DataManager dataManager { get; set; } = new();

        BattleField battleField;

        Image enemyImg = new Bitmap(Resources.NoobShip);
        Image playerImg = new Bitmap(Resources.PlayerShip);
        Image bulletImg = new Bitmap(Resources.Bullet);
        Image enemyBulletImg;

        int packetNumber = 0; // номер последнего принятого пакета GameObjectsInfo

        public GameForm(GameSocket _socket)
        {
            InitializeComponent();

            socket = _socket;
            battleField = new BattleField();
            enemyBulletImg = bulletImg;
            enemyBulletImg.RotateFlip(RotateFlipType.Rotate180FlipNone);

            labelLoading.Show();
            gameOverText.Hide();

            Shown += new EventHandler(StartConnectionToServer);
        }

        private void StartConnectionToServer(object? sender, EventArgs e)
        {
            Thread thread = new(() => {
                // ждем Enemies And Bullets Info
                int packetOpcode = -1;
                while (packetOpcode != (int)PacketOpcode.GameObjectsInfo)
                    packetOpcode = packetManager.ParsePacket(socket.ReceiveTcpPacket(), ref battleField, ref packetNumber);
            });
            thread.Start();
            thread.Join();

            // инициализируем игру
            InitGame();

            Thread dataReceiveThread = new(() => { ParallelDataReceive(); });
            dataReceiveThread.Start();
        }

        private void InitGame() // инициализация игры
        {
            // событие будет порождаться в потоке из пула потоков CLR по умолчанию
            gameTimer.Interval = TIMER_INTERVAL_MS;
            gameTimer.Enabled = true;
            gameTimer.Tick += Update; 
            gameTimer.Enabled = true;

            labelLoading.Hide();
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
                battleField.Player.KeyDown(false);
                socket.SendTcpPacket(packetManager.CreateKeyDownPacket(false));
            }
            else if (e.KeyCode == Keys.D) // вправо
            {
                battleField.Player.KeyDown(true);
                socket.SendTcpPacket(packetManager.CreateKeyDownPacket(true));
            }
            else if (e.KeyCode == Keys.Space) // выстрел
            {
                socket.SendTcpPacket(packetManager.CreateShotKeyDownPacket());
            }
        }
        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) // влево
            {
                battleField.Player.KeyUp();
                socket.SendTcpPacket(packetManager.CreateKeyUpPacket(false));
            }
            else if (e.KeyCode == Keys.D) // вправо
            {
                battleField.Player.KeyUp();
                socket.SendTcpPacket(packetManager.CreateKeyUpPacket(true));
            }
        }

        private void ParallelDataReceive()
        {
            while (Visible) {
                int opcode = packetManager.ParsePacket(socket.ReceiveTcpPacket(), ref battleField, ref packetNumber);
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

            dataManager.AddResult(battleField.Score);

            string msg =  $"\nTotal score: {battleField.Score}\nPress Enter to restart";
            gameOverText.Text = msg;
            gameOverText.Show();
        }

        private void gameOverText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Hide();

                Application.Exit();
                System.Diagnostics.Process.Start(Application.ExecutablePath);
            }
        }
    }
}
