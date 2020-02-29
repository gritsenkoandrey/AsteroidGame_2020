using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame_2020
{
    public class GameObjectException : Exception
    {
        public GameObjectException(string message)
            : base(message)
        {
            
        }
    }
}
