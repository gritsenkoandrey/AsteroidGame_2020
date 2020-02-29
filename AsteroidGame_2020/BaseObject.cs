using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace AsteroidGame_2020
{
    public abstract class BaseObject : ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        public Point Position => Pos;
        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public abstract void Draw(); // в абстрактном базовом классе методы должны быть без реализации, вся реализация идет в производных классах
        public abstract void Update();

        //Так как переданный объект тоже должен будет реализовывать интерфейс ICollision, мы
        //можем использовать его свойство Rect и метод IntersectsWith для обнаружения пересечения с
        //нашим объектом(а можно наоборот)
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect //public Rectangle Rec => new Rectangle(Pos, Size); можно вот так сократить
        {
            get { return new Rectangle(Pos, Size); }
        }
    }
}
