using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AsteroidGame_2020
{
    class Ship : BaseObject, ICollision
    {
        public static event Message MessageDie;

        private int _energy = 100;
        public int Energy => _energy;
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Rect);
            Game.Buffer.Graphics.DrawEllipse(Pens.Red, Rect);
        }
        public override void Update()
        {

        }
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Die()
        {
            MessageDie?.Invoke();
        }
        public void ChangeEnergy(int delta)
        {
            _energy += delta;
        }
        public bool CheckCollision(ICollision obj)
        {
            var is_collision = Rect.IntersectsWith(obj.Rect);
            if (is_collision && obj is Asteroid asteroid)
            {
                ChangeEnergy(- asteroid.Power);
            }
            if (is_collision && obj is FirstAidKit firstAidKit)
            {
                ChangeEnergy(+firstAidKit.Power);
            }
            return is_collision;
        }
    }
}
