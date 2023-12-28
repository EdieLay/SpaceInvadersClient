using SpaceInvadersClient.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersServer.GameObjects
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

        // массив пацанов: true - жив, false - мертв
        public bool[] boolEnemies { get; set; }
        int[] _x; // реальные координаты пацанов
        int[] _y;
        int _enemiesAlive;
        public int[] X { get => _x; }
        public int[] Y { get => _y; }
        public int EnemiesAlive { get => _enemiesAlive; }

        // смещение левого верхнего угла каждого пацана относительно его начальной позиции по икс
        public int offsetX { get; set; }
        // смещение левого верхнего угла каждого пацана относительно его начальной позиции по игрек
        public int offsetY { get; set; }
        // скорость пацанов по икс за тик таймера
        public int speed { get; set; }

        int downBorder;
        int upBorder;
        int leftBorder;
        int rightBorder;

        public Enemies(int fieldWidth, int fieldHeight)
        {
            boolEnemies = new bool[ROWS * COLS]; // 5 строк по 11 пацанов
            _enemiesAlive = ROWS * COLS;
            Array.Fill(boolEnemies, true);
            offsetX = 0;
            offsetY = 0;
            CalculateSpeed();
            FIELD_WIDTH = fieldWidth;
            FIELD_HEIGHT = fieldHeight;
            downBorder = ROWS * _HEIGHT + (ROWS - 1) * GAP_Y;
            upBorder = 0;
            leftBorder = 0;
            rightBorder = COLS * _WIDTH + (COLS - 1) * GAP_X;
        }

        public void Move() // нужно изменить
        {
            offsetX += speed;
            if (offsetX + _WIDTH * COLS + GAP_X * (COLS - 1) >= FIELD_WIDTH)
            {
                offsetY += _HEIGHT;
                offsetX = FIELD_WIDTH - _WIDTH * COLS + GAP_X * (COLS - 1);
                speed *= -1;
            }
            else if (offsetX < 0)
            {
                offsetY++;
                offsetX = 0;
                speed *= -1;
            }
        }

        void CalculateSpeed()
        {
            // enemiesAlive = 55 => speed = 4
            // enemiesAlive = 1  => speed = 36
            speed = (int)(243.0 / (_enemiesAlive + 5.75));
        }

        public void CalculateBulletCollision(Bullet bullet)
        {
            int bulX = bullet.X;
            int bulY = bullet.Y;
            int bulWidth = bullet.WIDTH;
            int bulHeight = bullet.HEIGHT;
            if (bulY > downBorder || bulY + bulHeight < upBorder ||
                bulX + bulWidth < leftBorder || bulX > rightBorder)
                return;
            // добавить проверку на столкновение
        }

        void GetBorders()
        {
            // Высчитывание границ живых пацанов
        }

        void ReplaceWithServerData()
        {
            // заменяет высчитанные на клиенте данные на данные, пришедшие с сервера
        }
    }
}
