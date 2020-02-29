using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AsteroidGame_2020
{
    public abstract class CollisionObject : BaseObject, ICollision
    {
        protected CollisionObject(Point pos, Point dir, Size size) : base(pos, dir, size) { }
        public bool CheckCollision(ICollision obj) => Rect.IntersectsWith(obj.Rect);
    }
}
