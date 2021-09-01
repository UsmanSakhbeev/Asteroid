using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroid
{
    class Bullet:BaseObject
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
            Pos.X = Pos.X + Dir.X;
            if (Pos.X > 800)
                Delete();
        }
        public void Shot(Point point)
        {
            Pos.X = point.X+50;
            Pos.Y = point.Y+32;
        }
        public void Delete()
        {
            
        }

    }
}
