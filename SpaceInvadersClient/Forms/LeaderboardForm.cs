namespace SpaceInvadersClient
{
    public partial class LeaderboardForm : Form
    {
        GameSocket socket { get; set; } // сокет для отправки и получения игровых данных
        PacketManager packetManager { get; set; } = new(); // класс для конвертации отправляющихся и полученных данных
        DataManager dataManager { get; set; } = new();
        MenuForm menuForm { get; set; }

        public LeaderboardForm(GameSocket _socket, MenuForm _menuForm)
        {
            InitializeComponent();

            socket = _socket;
            menuForm = _menuForm;

            labelLoading.Hide();
            textResults.Show();

            textResults.Text = dataManager.GetResults();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            menuForm.Show();
            this.Close();
        }
    }
}
