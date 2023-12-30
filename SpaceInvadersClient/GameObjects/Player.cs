namespace SpaceInvadersServer
{
    public class Player
    {
        readonly int FIELD_WIDTH; // ширина поля
        readonly int FIELD_HEIGHT; // высота поля

        const int _WIDTH = 60;
        const int _HEIGHT = 45;
        public int WIDTH { get => _WIDTH; }
        public int HEIGHT { get => _HEIGHT; }


        readonly int _Y;
        public int Y { get => _Y; }
        public int x { get; set; }
        public int _speed = 10;
        public int Speed { get => _speed; }

        bool flagMove = false;
        bool toRight;

        public Player(int fieldWidth, int fieldHeight)
        {
            FIELD_WIDTH = fieldWidth;
            FIELD_HEIGHT = fieldHeight;
            _Y = FIELD_HEIGHT - 2 * HEIGHT;
            x = FIELD_WIDTH / 2 - _WIDTH / 2;
        }

        public void KeyDown(bool _toRight)
        {
            flagMove = true;
            toRight = _toRight;
        }
        public void KeyUp()
        {
            flagMove = false;
        }

        public void Move()
        {
            if (!flagMove) return;
            if (toRight)
                x = x + _speed > FIELD_WIDTH + _WIDTH ? FIELD_WIDTH + _WIDTH : x + _speed;
            else
                x = x - _speed < 0 ? 0 : x - _speed;
        }

        // пока что столкновение с пацанами реализовано просто, как заход на один игрек с игроком
        public bool CalculateEnemyCollision(int downBorder)
        {
            if (downBorder > _Y)
                return true;
            return false;
        }
    }
}
