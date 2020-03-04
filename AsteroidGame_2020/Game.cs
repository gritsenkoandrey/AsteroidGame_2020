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

            _timer.Start();
            _timer.Tick += Timer_tick;

            form.KeyDown += Form_KeyDown;

            Ship.MessageDie += Finish;
        }
        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("!!! Игра окончена !!!",
                new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold),
                Brushes.Red, 5, 200);
            Buffer.Render();
        }
        private static void Form_KeyDown(object sender, KeyEventArgs e) // управление кораблем
        {
            if (e.KeyCode == Keys.ControlKey) _bullet = new Bullet(_ship.Rect.Y+2);
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
            foreach (BaseObject obj in _game_object)
                obj?.Draw();
            _ship?.Draw();
            _bullet?.Draw();
            // вывод энергии корабля
            if (_ship != null)
                Buffer.Graphics.DrawString("Энергия корабля:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 10, 10);
            Buffer.Render();
        }
        public static BaseObject[] _game_object;
        public static Bullet _bullet;
        public static Ship _ship;        
        public static void Load()
        {
            var game_object = new List<BaseObject>();

            var rnd = new Random();

            const int asteroids_count = 20;
            const int stars_count = 300;
            const int star_size = 2;
            
            _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));
            //_bullet = new Bullet(200); // изначально будем без пули

            for (int i = 0; i < asteroids_count; i++)
            {
                int r = rnd.Next(5, 50);
                game_object.Add(new Asteroid(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(-r, 0), new Size(r, r)));
            }
            for (int i = 0; i < stars_count; i++)
            {
                int r = rnd.Next(5, 50);
                game_object.Add(new Star(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(-r, 0), new Size(star_size, star_size)));
            }
            _game_object = game_object.ToArray();
        }
        public static void Update()
        {
            foreach (BaseObject obj in _game_object)
                obj?.Update();
            _bullet?.Update();
            for (int i = 0; i < _game_object.Length; i++)
            {
                if (_game_object[i] == null) continue;
                _game_object[i].Update();                
                if (_bullet != null && _bullet.Collision(_game_object[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _bullet = null;
                    _game_object[i] = null;
                    continue;
                }
                if (!_ship.Collision(_game_object[i])) continue;
                var rnd = new Random();
                _ship?.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship.Die();
            }            
        }
    }
}
