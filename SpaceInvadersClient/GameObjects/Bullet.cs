using SpaceInvadersClient.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersServer
{
    public class Bullet
    {
        const int _WIDTH = 2;
        const int _HEIGHT = 5;
        public int WIDTH { get => _WIDTH; }
        public int HEIGHT { get => _HEIGHT; }

        int _x; // координаты левого верхнего угла пули
        int _y;
        int _speed;
        public int X { get => _x; }
        public int Y { get => _y; }

        public Bullet(int x, int y, int speed)
        {
            this._x = x;
            this._y = y;
            _speed = speed;
        }
    }
}
