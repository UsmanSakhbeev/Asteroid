using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroid
{
    class Bullet:BaseObject, IReplace
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {            
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Properties.Resources.laserBullet, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X +10;
            if (Pos.X > 800)
                Replace();
        }
        public void Replace()
        {
            Random rnd = new Random();
            Pos.X = 0;
            Pos.Y = rnd.Next(100, 800);
        }
    }
}
