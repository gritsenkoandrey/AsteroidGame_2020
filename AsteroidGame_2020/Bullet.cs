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
        public Bullet(int Pos)
            : base(new Point(0, Pos), Point.Empty, new Size(20, 5))
        {

        }
        //public Bullet (Point pos, Point dir, Size size) : base (pos, dir, size) { }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.Red, Rect);
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Rect);
        }
        public override void Update()
        {
            Pos = new Point(Pos.X + 3, Pos.Y);
            //Pos.X = Pos.X + 3;
        }
    }
}
