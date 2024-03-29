﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroid
{
    class Bullet : BaseObject
    {
        private bool _isCornerTouched;
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            _isCornerTouched = false;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Properties.Resources.laserBullet, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X > 800)
                _isCornerTouched = true;
        }
        public void Shot(Point point)
        {
            Pos.X = point.X + 50;
            Pos.Y = point.Y + 32;
        }
        public bool IsCornerTouchedChecking()
        {
            return _isCornerTouched;
        }

    }
}
