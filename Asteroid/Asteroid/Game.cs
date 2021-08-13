using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroid
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        private static int _height;
        private static int _width;
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
            Timer timer = new Timer { Interval = 50 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        public static void Draw()
        {            
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
                obj.Draw();
            foreach (BaseObject obj in _asteroids)
                obj.Draw();
            _bullet.Draw();

            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();
            foreach (Asteroid asteroid in _asteroids)
            {
                asteroid.Update();
                if(asteroid.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    asteroid.Replace();
                    _bullet.Replace();
                }
            }

            _bullet.Update();
        }
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;
        public static BaseObject[] objs;
        private static Random rnd = new Random();
        public static void Load()
        {
            _bullet = new Bullet(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(-20, 0), new Size(25, 7));

            
            _asteroids = new Asteroid[5];
            for (int i = 0; i <_asteroids.Length; i++)
            {
                int r = rnd.Next(30, 60);
                _asteroids[i] = new Asteroid(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(r/5,r), new Size(r,r));
            }

            objs = new BaseObject[60];
            for (int i = 0; i < 40; i++)
                objs[i] = new Kamet(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(i / 4, 0), new Size(5, 5));
            objs[40] = new SunBlue(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(1, 0), new Size(100, 100));            
            for (int i = 41; i < objs.Length-1; i++)
                objs[i] = new Star(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(10, -2 - i / 5), new Size(7, 7));
            objs[59] = new Planet(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(2, 0), new Size(200, 200));
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
    }
}
