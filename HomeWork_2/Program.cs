using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //создаем массив сотрудников
            BaseWorker[] workers = new FirstWorker[100];

            var rnd = new Random();

            //заполняем массив сотрудников
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i] = new FirstWorker()
                { Name = $"{i + 1}", Age = rnd.Next(18, 65) };
            }

            //сортируем массив
            Array.Sort(workers);

            //выводим данные массива на кончоль с помощью цикла foreach
            foreach (BaseWorker w in workers)
            {
                Console.WriteLine(w);
            }

            Console.ReadKey();
        }
    }
}

