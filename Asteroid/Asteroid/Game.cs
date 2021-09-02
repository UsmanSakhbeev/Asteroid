using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;


namespace Asteroid
{
    delegate void scoreCounterDel();
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        private static int _height;
        private static int _width;
        public static int score;
        public static scoreCounterDel ScoreCounterDelegate = ScoreCounter;
        public static int Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value > 1000 || value < 1)
                    throw new ArgumentOutOfRangeException("Incorrect scale of form");
                else
                    _width = value;
            }
        }
        public static int Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value > 1000 || value < 1)
                    throw new ArgumentOutOfRangeException("Incorrect scale of form");
                else
                    _height = value;
            }
        }

        static Game()
        {
        }
        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = 10 };
            timer.Start();
            timer.Tick += Timer_Tick;
            form.KeyDown += OnKeyDown;
            form.KeyUp += OnKeyUp;
            score = 0;
        }
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (BaseObject asteroid in _asteroids)
                asteroid.Draw();
            foreach (Bullet bullet in _bullets)
            {
                bullet.Draw();
            }
            _spaceShip.Draw();
            _energyBooster.Draw();
            Buffer.Render();

        }

        private static List<int> _bulletIndexesToDelete = new List<int>();        
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (Asteroid asteroid in _asteroids)
            {
                asteroid.Update();
                if (asteroid.Collision(_spaceShip))
                {
                    asteroid.Replace();
                    _spaceShip.ChangeHP(asteroid.Power);
                }                    
                foreach (Bullet bullet in _bullets)
                {
                    if (asteroid.Collision(bullet))
                    {
                        ScoreCounterDelegate();
                        asteroid.Replace();
                        _bulletIndexesToDelete.Add(_bullets.IndexOf(bullet));
                    }
                }
                foreach (int item in _bulletIndexesToDelete)
                {
                    _bullets.RemoveAt(item);
                }
                _bulletIndexesToDelete.Clear();
            }
            if (_bullets.Count > 0)
            {
                foreach (Bullet bullet in _bullets)
                {
                    bullet.Update();
                }
                foreach (Bullet bullet in _bullets)
                {
                    if(bullet.IsCornerTouchedChecking())
                        _bulletIndexesToDelete.Add(_bullets.IndexOf(bullet));
                }
                foreach (int item in _bulletIndexesToDelete)
                {
                    _bullets.RemoveAt(item);
                }
                _bulletIndexesToDelete.Clear();
            }
            _spaceShip.Update();
            _energyBooster.Update();
            if (_energyBooster.Collision(_spaceShip))
            {
                _spaceShip.ChangeHP(_energyBooster.Power);
                _energyBooster.Replace();
            }

            //if(_spaceShip.Collision(as))
        }
        private static Asteroid[] _asteroids;
        private static BaseObject[] _objs;
        private static Random rnd = new Random();
        private static SpaceShip _spaceShip;
        private static List<Bullet> _bullets;
        private static EnergyBooster _energyBooster;
        public static void Load()
        {
            _spaceShip = new SpaceShip(new Point(0, _height / 2), new Point(10, 10), new Size(70, 70), 100);
            _bullets = new List<Bullet>();
            _energyBooster = new EnergyBooster(new Point(800, rnd.Next(50, 450)), new Point(-5, 0), new Size(30, 30));
            _asteroids = new Asteroid[5];

            for (int i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(30, 60);
                _asteroids[i] = new Asteroid(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(r / 5, r), new Size(r, r));
            }
            _objs = new BaseObject[60];
            for (int i = 0; i < 40; i++)
                _objs[i] = new Comet(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(i / 4, 0), new Size(5, 5));
            _objs[40] = new SunBlue(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(1, 0), new Size(100, 100));
            for (int i = 41; i < _objs.Length - 1; i++)
                _objs[i] = new Star(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(10, -2 - i / 5), new Size(7, 7));
            _objs[59] = new Planet(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(2, 0), new Size(200, 200));

        }

        private static void OnKeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Space)
            {
                Point pos = _spaceShip.GiveCoordinates();
                _bullets.Add(new Bullet(new Point(pos.X + 50, pos.Y + 32), new Point(15, 0), new Size(25, 7)));
                _bullets[_bullets.Count - 1].Draw();

            }
            if (e.KeyCode == Keys.W)
            {
                _spaceShip.Up();
                _spaceShip.dir = SpaceShip.direction.Up;
            }
            if (e.KeyCode == Keys.S)
            {
                _spaceShip.Down();
                _spaceShip.dir = SpaceShip.direction.Down;
            }
        }
        private static void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.S)
            {
                _spaceShip.dir = SpaceShip.direction.Straight;
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        private static void ScoreCounter()
        {
            System.Media.SystemSounds.Hand.Play();
            score++;
            Console.WriteLine("Очки: " + score);
            using (StreamWriter streamWriter = new StreamWriter("output.txt"))
            {
                streamWriter.Write("Очки: " + score);
            }
        }

    }
}
