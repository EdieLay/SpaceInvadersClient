namespace SpaceInvadersServer
{
    public class Bullet
    {
        const int _WIDTH = 6;
        const int _HEIGHT = 15;
        public int WIDTH { get => _WIDTH; }
        public int HEIGHT { get => _HEIGHT; }

        int _x; // координаты левого верхнего угла пули
        int _y;
        int _speed;
        public int X { get => _x; }
        public int Y { get => _y; }
        public bool IsAlive { get; set; }

        public Bullet(int x, int y, int speed, bool _isAlive = false)
        {
            this._x = x;
            this._y = y;
            _speed = speed;
            IsAlive = _isAlive;
        }

        public void Move()
        {
            _y += _speed;
        }
    }
}
