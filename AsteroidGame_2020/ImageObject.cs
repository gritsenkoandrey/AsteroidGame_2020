using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AsteroidGame_2020
{
    class ImageObject : BaseObject
    {
        public ImageObject(Point pos, Point dir, Size size, Image image) : base (pos, dir, size)
        {
            this.image = image;
        }

        Image image = Properties.Resources.Asteroid;

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
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
    }
}
