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
        public static int Width { get; set; }
        public static int Height { get; set; }
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
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            Buffer.Render();

            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
                obj.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();
        }

        public static BaseObject[] objs;
        private static Random rnd = new Random();
        public static void Load()
        {
            objs = new BaseObject[60];
            for (int i = 0; i < objs.Length / 2; i++)
                objs[i] = new BaseObject(new Point(rnd.Next(0,800), rnd.Next(0,600)), new Point(i, 0), new Size(7, 7));
            for (int i = objs.Length/2; i < objs.Length; i++)
                objs[i] = new Star(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(i/4, 0), new Size(5, 5));
            //for (int i = 0; i < objs.Length; i++)
                //objs[i] = new BaseObject(new Point(600, i * 20), new Point(-i, -i), new Size(10, 10));

        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
    }
}
