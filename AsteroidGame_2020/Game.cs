using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace AsteroidGame_2020
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        static Game()
        {
        }
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
                        
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_tick;
        }
        public static void Timer_tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        public static void Draw()
        {
            // Проверяем вывод графики
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            Buffer.Render();

            Buffer.Graphics.Clear(Color.Black);
            foreach(BaseObject obj in _objs)
            {
                obj.Draw();                
            }
            Buffer.Render();
        }
        public static BaseObject[] _objs;
        public static void Load()
        {           
            _objs = new BaseObject[60];
            for (int i = 0; i < _objs.Length/3; i++)
                _objs[i] = new BaseObject(new Point(600, i * 20), new Point(15-i, 20-i), new Size(10, 10));
            for (int i = _objs.Length/3; i < _objs.Length-_objs.Length / 3; i++)
                _objs[i] = new Star(new Point(600, i * 20), new Point(-i, 0), new Size(5, 5));
            for (int i = _objs.Length - _objs.Length / 3; i < _objs.Length; i++)
                _objs[i] = new Rectangles(new Point(600, i), new Point(20-i, 20-i), new Size(1, 1));
        }
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }
    }
}
