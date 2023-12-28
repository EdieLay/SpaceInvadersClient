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
    public partial class LeaderboardForm : Form
    {
        GameSocket socket { get; set; } // сокет для отправки и получения игровых данных
        PacketManager packetManager { get; set; } // класс для конвертации отправляющихся и полученных данных

        public LeaderboardForm(GameSocket _socket, PacketManager _packetManager)
        {
            InitializeComponent();

            socket = _socket;
            packetManager = _packetManager;

            labelLoading.Visible = true;

            // ждем ???
            //int packetOpcodeNumber = -1;
            //while (packetOpcodeNumber != (int)PacketOpcode.EnemiesAndBulletsInfo)
            //    packetOpcodeNumber = packetManager.ParsePacket(socket.ReceivePacket());

            // начинаем отображать
            labelLoading.Visible = false;
        }
    }
}
