using System.Drawing;

namespace Asteroid
{
    class Comet: BaseObject
    {
        public Comet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Properties.Resources.star2, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;

            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;

        }
    }
}
