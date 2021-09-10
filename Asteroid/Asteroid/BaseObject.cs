using System.Drawing;

namespace Asteroid
{
    abstract class BaseObject : ICollision
    {
        protected Point Pos;
        private Point _dir;
        private Size _size;
        /*protected Point Pos
        {
            get { return _pos; }
            set
            {
                if (value.X < 0)
                    throw new BaseObjectException(Pos.X.ToString(), "Incorrect position");
                else if (value.Y < 0)
                    throw new BaseObjectException(Pos.Y.ToString(), "Incorrect position");
                else
                {
                    _pos.X = value.X;
                    _pos.Y = value.Y;
                    //_pos = value;
                }
            }
        }*/
        protected Point Dir
        {
            get { return _dir; }
            set { _dir = value; }
        }
        protected Size Size
        {
            get { return _size; }
            set
            {
                if (value.Width > 250)
                    throw new BaseObjectException(value.Width.ToString(), "Too big size of object");
                else if (value.Height > 250)
                    throw new BaseObjectException(value.Height.ToString(), "Too big size of object");
                else
                {
                    _size = value;
                }
            }
        }

        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public abstract void Draw();

        public abstract void Update();


        public bool Collision(ICollision o)
        {
            return o.Rect.IntersectsWith(this.Rect);
        }

        public Rectangle Rect => new Rectangle(Pos, Size);
    }
}
