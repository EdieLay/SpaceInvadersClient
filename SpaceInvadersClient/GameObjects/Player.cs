﻿using SpaceInvadersClient.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersServer
{
    public class Player
    {
        readonly int FIELD_WIDTH; // ширина поля
        readonly int FIELD_HEIGHT; // высота поля

        const int _WIDTH = 20;
        const int _HEIGHT = 15;
        public int WIDTH { get => _WIDTH; }
        public int HEIGHT { get => _HEIGHT; }


        readonly int _Y;
        public int Y { get => _Y; }
        public int x { get; set; }
        public int _speed = 10;
        public int Speed { get => _speed; }

        public Player(int fieldWidth, int fieldHeight)
        {
            FIELD_WIDTH = fieldWidth;
            FIELD_HEIGHT = fieldHeight;
            _Y = FIELD_HEIGHT - 2 * HEIGHT;
            x = FIELD_WIDTH / 2 - _WIDTH / 2;
        }

        public void Move(bool toRight)
        {
            if (toRight)
                x = x + _speed > FIELD_WIDTH + _WIDTH ? FIELD_WIDTH + _WIDTH : x + _speed;
            else
                x = x - _speed < 0 ? 0 : x - _speed;
        }
    }
}
