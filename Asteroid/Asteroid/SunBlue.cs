using System.Drawing;

namespace Asteroid
{
    class SunBlue:BaseObject
    {
        public SunBlue(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Properties.Resources.SunBlue, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < -100) Pos.X = Game.Width + Size.Width;
        }
    }
}
