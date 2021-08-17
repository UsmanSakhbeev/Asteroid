using System;
using System.Drawing;


namespace Asteroid
{
    class SpaceShip:BaseObject
    {
        private int _hitPoints;
        public enum direction { Up, Down, Straight}
        public direction dir = direction.Straight;
        public int HitPoints
        {
            get { return _hitPoints; }
            set
            {
                if (value > 0)
                    _hitPoints = value;
                else
                    throw new ArgumentOutOfRangeException(Convert.ToString(value), "SpaceShip hitpoints must be positive");
            }
        }
        public SpaceShip(Point pos, Point dir, Size size, int hitPoints) : base(pos, dir, size)
        {
            HitPoints = hitPoints;
        }

        public override void Draw()
        {
            if (dir == direction.Straight)
            {
                Game.Buffer.Graphics.DrawImage(Properties.Resources.spaceShipStraight, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
            if (dir == direction.Up)
            {
                Game.Buffer.Graphics.DrawImage(Properties.Resources.spaceShipUp, Pos.X, Pos.Y - 5, Size.Width, Size.Height);
            }
            if (dir == direction.Down)
            {
                Game.Buffer.Graphics.DrawImage(Properties.Resources.spaceShipDown, Pos.X, Pos.Y + 5, Size.Width, Size.Height);
            }
        }

        public override void Update()
        {

        }

        public void Up()
        {
            if(Pos.Y>=13)
                Pos.Y -= Dir.Y;
        }

        public void Down()
        {
            if(Pos.Y<=475)
                Pos.Y += Dir.Y;
        }


    }
}
