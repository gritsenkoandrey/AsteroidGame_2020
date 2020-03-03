using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AsteroidGame_2020
{
    class Bullet : BaseObject
    {
        public Bullet (Point pos, Point dir, Size size) : base (pos, dir, size) { }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.Red, Rect);
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Rect);
        }
        public override void Update()
        {
            Pos.X = Pos.X + 3;
        }
    }
}
