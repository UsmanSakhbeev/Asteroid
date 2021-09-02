using System;
using System.Drawing;
namespace Asteroid
{
    class EnergyBooster:BaseObject
    {
        public int Power { get; set; }
        public EnergyBooster(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 15;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Properties.Resources.EnergyBooster, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X += Dir.X;
            if (Pos.X < -50)
                Replace();
        }
        public void Replace()
        {
            Random random = new Random();
            Pos.X = 800;
            Pos.Y = random.Next(50, 450);
        }
    }
}
