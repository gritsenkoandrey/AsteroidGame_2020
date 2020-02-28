using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2
{
    class FirstWorker : BaseWorker
    {
        readonly decimal fix_price = 8000; // фиксированная оплата
        public FirstWorker() { }
        public FirstWorker(string name, int age) : base (name, age) { }
        protected override decimal Wage()
        {
            return fix_price;
        }
    }
}
