using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2
{
    public abstract class BaseWorker : IComparable
    {
        public string Name { get; set; }

        public int Age { get; set; }

        protected BaseWorker() { }
        protected BaseWorker(string name, int age)
        {
            name = Name;
            age = Age;
        }
        protected abstract decimal Wage(); //абстрактный метод для расчета среднемесячной зарплаты
        public override string ToString() => $"Рабочий - {Name} : Возраст - {Age}"; // переопределяем метод ToString

        public int CompareTo(object obj) // определяем метод сортировки в интерфейсе IComparable
        {
            if (Age < ((BaseWorker)obj).Age) return -1;
            if (Age > ((BaseWorker)obj).Age) return 1;
            return 0;
        }
    }
}
