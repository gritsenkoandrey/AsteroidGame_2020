using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2
{
    class SecondWorker : BaseWorker
    {
        readonly decimal hour_price = 50; //почасовая оплата
        public SecondWorker() { }
        public SecondWorker(string name, int age) : base (name, age) { }
        protected override decimal Wage()
        {
            decimal wage_price = (int)20.8 * 8 * hour_price;
            return wage_price;
        }
    }
}
