using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace AsteroidGame_2020
{
    abstract class BaseObject
    {
        public delegate void Message();

        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public abstract void Draw(); // в абстрактном базовом классе методы должны быть без реализации, вся реализация идет в производных классах
        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
        // Так как переданный объект тоже должен будет реализовывать интерфейс ICollision, мы 
        // можем использовать его свойство Rect и метод IntersectsWith для обнаружения пересечения с
        // нашим объектом (а можно наоборот)
        //public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect //public Rectangle Rec => new Rectangle(Pos, Size); можно вот так сокрвтить
        {
            get { return new Rectangle(Pos, Size); }
        }
    }
}
