namespace SpaceInvadersServer
{
    public class BattleField
    {
        const int WIDTH = 600;
        const int HEIGHT = 800;
        Enemies _enemies;
        List<Bullet> _enemyBullets;
        Player _player;

        public Enemies Enemies { get => _enemies; }
        public List<Bullet> EnemyBullets { get => _enemyBullets; }
        public Bullet PlayerBullet { get; set; } = new Bullet(0, 0, 0, false);
        public Player Player { get => _player; }
        public int Score { get; set; } = 0;

        public BattleField()
        {
            _enemies = new Enemies(WIDTH, HEIGHT);
            _enemyBullets = new List<Bullet>();
            _player = new Player(WIDTH, HEIGHT);
        }

        public void Update() // тут происходит вся логика игры
        {
            _enemies.Move();
            for (int i = 0; i < _enemyBullets.Count; i++)
                _enemyBullets[i].Move();

            _player.Move();
            if (PlayerBullet != null) PlayerBullet.Move();

            _enemies.CalculateBulletCollision(PlayerBullet);
            //_player.CalculateBulletsCollision(_enemyBullets);
        }
    }
}
