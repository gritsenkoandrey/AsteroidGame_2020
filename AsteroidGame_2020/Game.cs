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

        private static Timer _timer = new Timer() { Interval = 10 };
        public static Random Rnd = new Random();

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

            //Timer timer = new Timer { Interval = 100 };
            _timer.Start();
            _timer.Tick += Timer_tick;

            form.KeyDown += Form_KeyDown;

            Ship.MessageDie += Finish;
        }
        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("Конец игры!", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
        private static void Form_KeyDown(object sender, KeyEventArgs e) // управление кораблем
        {
            if (e.KeyCode == Keys.ControlKey) _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(5, 2));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        public static void Timer_tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj?.Draw();
            foreach (Asteroid obj in _asteroids)
                obj?.Draw();
            _ship?.Draw();
            _bullet?.Draw();
            // если корабль живой, то выводим его энергию на экран
            if (_ship != null)
                Buffer.Graphics.DrawString("Энергия корабля:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 10, 10);
            Buffer.Render();
        }
        public static BaseObject[] _objs;
        public static Asteroid[] _asteroids;
        public static Bullet _bullet;
        public static Ship _ship;
        public static void Load()
        {
            _objs = new BaseObject[200];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(5, 2));
            _asteroids = new Asteroid[15];
            _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));

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
                obj.Update();
            _bullet?.Update();
            for (int i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();                
                if (_bullet != null && _bullet.Collision(_asteroids[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _bullet = null;
                    _asteroids[i] = null;
                    continue;
                }
                if (!_ship.Collision(_asteroids[i])) continue;
                var rnd = new Random();
                //_ship?.EnergyLow(rnd.Next(1, 10));
                //System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship.Die();
            }            
        }
    }
}
