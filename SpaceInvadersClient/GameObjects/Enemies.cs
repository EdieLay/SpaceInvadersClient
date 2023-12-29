namespace SpaceInvadersServer
{
    public class Enemies
    {
        const int _WIDTH = 20; // ширина пацана
        const int _HEIGHT = 15; // высота пацана
        public int WIDTH { get => _WIDTH; }
        public int HEIGHT { get => _HEIGHT; }

        const int ROWS = 5; // количество строк пацанов
        const int COLS = 11; // количество столбцов пацанов
        const int GAP_X = 3; // расстояние от правого конца пацана до следующего пацана
        const int GAP_Y = _HEIGHT; // расстояние от нижнего конца пацана до следующего пацана
        readonly int FIELD_WIDTH; // ширина поля
        readonly int FIELD_HEIGHT; // высота поля

        public bool[] boolEnemies { get; set; } // массив пацанов: true - жив, false - мертв
        int[] _x; // реальные координаты пацанов
        int[] _y;
        int _enemiesAlive;
        public int[] X { get => _x; }
        public int[] Y { get => _y; }
        public int EnemiesAlive { get => _enemiesAlive; }

        public int offsetX { get; set; } // смещение левого верхнего угла каждого пацана относительно его начальной позиции по икс
        public int offsetY { get; set; } // смещение левого верхнего угла каждого пацана относительно его начальной позиции по игрек
        public int speed { get; set; } // скорость пацанов по икс за тик таймера

        int downBorder; // координата y нижней границы
        int upBorder; // координаты y верхней границы
        int leftBorder; // координата x левой границы
        int rightBorder; // координата x правой границы

        int downBorderNum; // номер нижней границы пацанов
        int upBorderNum; // номер верхней
        int leftBorderNum; // номер левой
        int rightBorderNum; // номер правой

        int wave; // текущая волна

        public Enemies(int fieldWidth, int fieldHeight)
        {
            boolEnemies = new bool[ROWS * COLS]; // 5 строк по 11 пацанов
            _enemiesAlive = ROWS * COLS;
            Array.Fill(boolEnemies, true);

            offsetX = 0;
            offsetY = 0;
            FIELD_WIDTH = fieldWidth;
            FIELD_HEIGHT = fieldHeight;

            downBorder = ROWS * _HEIGHT + (ROWS - 1) * GAP_Y;
            upBorder = 0;
            leftBorder = 0;
            rightBorder = COLS * _WIDTH + (COLS - 1) * GAP_X;

            downBorderNum = ROWS - 1;
            upBorderNum = 0;
            leftBorderNum = 0;
            rightBorderNum = COLS - 1;

            wave = 1;
        }

        public void Move()
        {
            if (_x[rightBorderNum] + WIDTH + speed >= FIELD_WIDTH)
            {
                for (int i = 0; i < _x.Length; i++)
                    _y[i] += HEIGHT;
                for (int i = rightBorderNum; i >= leftBorderNum; i--)
                    _x[i] = FIELD_WIDTH + WIDTH * (i - rightBorderNum - 1) + GAP_X * (i - rightBorderNum);
                speed *= -1;
            }
            else if (_x[leftBorder] < 0)
            {
                for (int i = 0; i < _x.Length; i++)
                    _y[i] += HEIGHT;
                for (int i = leftBorderNum; i <= rightBorderNum; i++)
                    _x[i] = WIDTH * (i - leftBorderNum) + GAP_X * (i - leftBorderNum);
                speed *= -1;
            }
            else
            {
                for (int i = 0; i < _x.Length; i++)
                    _x[rightBorderNum] += speed;
            }
        }

        public int CalculateBulletCollision(Bullet bullet)
        {
            if (!bullet.IsAlive) return 0;

            int bulX = bullet.X;
            int bulY = bullet.Y;
            int bulWidth = bullet.WIDTH;
            int bulHeight = bullet.HEIGHT;
            if (bulY > downBorder || bulY + bulHeight < upBorder ||
                bulX + bulWidth < leftBorder || bulX > rightBorder)
                return 0;

            int x, y;
            for (int i = upBorderNum; i <= downBorderNum; i++)
            {
                for (int j = leftBorderNum; j <= rightBorderNum; j++)
                {
                    if (!boolEnemies[i * COLS + j]) continue;
                    x = j * (WIDTH + GAP_X);
                    y = i * (HEIGHT + GAP_Y);
                    if (x > bulX + bulWidth || x + WIDTH < bulX ||
                        y > bulY + bulHeight || y + HEIGHT < bulY)
                        continue;
                    boolEnemies[i * COLS + j] = false;
                    _enemiesAlive--;
                    bullet.IsAlive = false;
                    GetBorders();
                    if (_enemiesAlive == 0)
                    {
                        StartNewWave();
                        return wave - 1;
                    }
                    return wave;
                }
            }
            return 0;
        }

        void GetBorders()
        {
            if (_enemiesAlive == 0) return;
            // Высчитывание границ живых пацанов
            int i = upBorderNum * COLS + leftBorderNum;
            while (!boolEnemies[i])
            {
                if (i % COLS == rightBorderNum)
                {
                    upBorderNum++;
                    i = upBorderNum * COLS + leftBorderNum;
                }
                else i++;
            }

            i = downBorderNum * COLS + leftBorderNum;
            while (!boolEnemies[i])
            {
                if (i % COLS == rightBorderNum)
                {
                    downBorderNum--;
                    i = downBorderNum * COLS + leftBorderNum;
                }
                else i++;
            }

            i = upBorderNum * COLS + leftBorderNum;
            while (!boolEnemies[i])
            {
                if (i / COLS == downBorderNum)
                {
                    leftBorderNum++;
                    i = upBorderNum * COLS + leftBorderNum;
                }
                i += COLS;
            }

            i = upBorderNum * COLS + rightBorderNum;
            while (!boolEnemies[i])
            {
                if (i / COLS == downBorderNum)
                {
                    rightBorderNum--;
                    i = upBorderNum * COLS + rightBorderNum;
                }
                i += COLS;
            }

            upBorder = upBorderNum * (HEIGHT + GAP_Y);
            downBorder = downBorderNum * (HEIGHT + GAP_Y) + HEIGHT;
            leftBorder = leftBorderNum * (WIDTH + GAP_X);
            rightBorder = rightBorderNum * (WIDTH + GAP_X) + WIDTH;
        }

        void StartNewWave()
        {
            _enemiesAlive = ROWS * COLS;
            Array.Fill(boolEnemies, true);
            offsetX = 0;
            offsetY = 0;
            downBorderNum = ROWS - 1;
            upBorderNum = 0;
            leftBorderNum = 0;
            rightBorderNum = COLS - 1;
            downBorder = ROWS * HEIGHT + (ROWS - 1) * GAP_Y;
            upBorder = 0;
            leftBorder = 0;
            rightBorder = COLS * WIDTH + (COLS - 1) * GAP_X;
            wave++;
        }
    }
}
