using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace AsteroidGame_2020
{
    interface ICollision
    {
        bool CheckCollision(ICollision obj);
        Rectangle Rect { get; }
    }
}
