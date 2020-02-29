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
            //Buffer.Graphics.Clear(Color.Black);
            //Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            //Buffer.Render();

            Buffer.Graphics.Clear(Color.Black);
            foreach(BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid obj in _asteroids)
                obj?.Draw();
            _bullet.Draw();
            Buffer.Render();
        }
        public static BaseObject[] _objs;
        public static Asteroid[] _asteroids;
        public static Bullet _bullet;
        public static void Load()
        {           
            _objs = new BaseObject[200];
            _bullet = new Bullet(200);
            _asteroids = new Asteroid[15];

            var rnd = new Random();

            for (int i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(-r, r), new Size(r, r));
            }   
            for (int i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(-r, r), new Size(2, 2));
            }               
        }
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj?.Update();
            foreach(Asteroid a in _asteroids)
                a?.Update();
            _bullet.Update();
            var rnd = new Random();

            if (_bullet.Position.X > Width)
                _bullet = new Bullet(new Random().Next(Width));

            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                var obj = _asteroids[i];
                if (obj is ICollision)
                {
                    var collision_object = (ICollision)obj;
                    if (_bullet.Collision(collision_object))
                    {
                        _bullet = new Bullet(new Random().Next(Width));
                        _asteroids[i] = null;
                        _asteroids[i] = new Asteroid(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(-r, r), new Size(r, r));
                        System.Media.SystemSounds.Hand.Play();
                    }
                }
            }
            
        }
    }
}
