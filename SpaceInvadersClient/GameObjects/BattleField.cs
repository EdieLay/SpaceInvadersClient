using SpaceInvadersServer.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersServer
{
    public class BattleField
    {
        const int WIDTH = 600;
        const int HEIGHT = 800;
        Enemies _enemies;
        List<Bullet> _enemyBullets;
        Bullet? _playerBullet;
        Player _player;
        int score;

        public Enemies Enemies { get => _enemies; }
        public List<Bullet> EnemyBullets { get => _enemyBullets; }
        public Bullet? PlayerBullet { get => _playerBullet; }
        public Player Player { get => _player; }
        public int Score { get => score; }

        public BattleField()
        {
            _enemies = new Enemies(WIDTH, HEIGHT);
            _enemyBullets = new List<Bullet>();
            _playerBullet = null;
            _player = new Player(WIDTH, HEIGHT);
            score = 0;
        }

        public void Update() // тут происходит вся логика игры
        {
            //enemies.Move();
            //for (int i = 0; i < enemyBullets.Count; i++)
            //    enemyBullets.Move();
            //playerBullet.Move();
        }

        public void UpdatePlayer(/*params*/)
        {
            //player.Move(/*params*/);
        }

        public void Sync() // тут происходит сихнронизация enemies, enemyBullets, player, playerBullet
        {
            //enemies
            //enemyBullets
            //playerBullet
        }
    }
}
