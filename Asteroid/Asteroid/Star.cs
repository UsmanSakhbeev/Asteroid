using System.Drawing;

namespace Asteroid
{
    class Star : BaseObject
    {
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        { 
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Properties.Resources.star_4, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            //Pos.Y = Pos.Y - Dir.Y;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
            //if (Pos.Y > Game.Height) Pos.Y = 0 + Size.Height;
        }
    }
}
