﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AsteroidGame_2020
{
    class FirstAidKit : BaseObject, ICollision
    {
        public int Power { get; set; }
        public FirstAidKit(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 10;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Properties.Resources.FirstAidKit, Rect);
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
        public bool CheckCollision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
    }
}
