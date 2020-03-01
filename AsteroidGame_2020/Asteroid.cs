using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AsteroidGame_2020
{
    class Asteroid : BaseObject, ICloneable, IComparable<Asteroid>
    {
        public int Power { get; set; } = 3;

        public object Clone() //реализация метода в интерфейсе ICloneable
        {
            // Создаем копию нашего робота
            Asteroid asteroid = new Asteroid(new Point(Pos.X, Pos.Y),
                new Point(Dir.X, Dir.Y),
                new Size(Size.Height, Size.Width))
            { Power = Power};
            // Не забываем скопировать новому астероиду Power нашего астероида
            //asteroid.Power = Power;
            return asteroid;
        }
        public Asteroid (Point pos, Point dir, Size size) : base (pos, dir, size)
        {
            Power = 1;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Properties.Resources.Asteroid, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update() // также можно воспользоваться реализацией базового класса
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
        int IComparable<Asteroid>.CompareTo(Asteroid obj)
        {
            if (Power > obj.Power)
                return 1;
            if (Power < obj.Power)
                return -1;
            return 0;
        }
    }
}
