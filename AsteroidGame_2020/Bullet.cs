using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AsteroidGame_2020
{
    class Bullet : BaseObject, ICollision
    {
        public Bullet(int Pos)
            : base(new Point(10, Pos), Point.Empty, new Size(20, 5)) { }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.Red, Rect);
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Rect);
        }
        public override void Update()
        {
            Pos = new Point(Pos.X + 20, Pos.Y);
        }
        public bool CheckCollision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
    }
}