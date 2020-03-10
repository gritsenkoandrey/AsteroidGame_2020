using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace AsteroidGame_2020
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }

        private static Timer _timer = new Timer() { Interval = 10 };
        public static Random Rnd = new Random();

        public static event Action<string> Log; //создаем событие
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
            Width = form.Width;
            Height = form.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            _timer.Start();
            _timer.Tick += Timer_tick;

            form.KeyDown += Form_KeyDown;

            Ship.MessageDie += Finish;

            Log?.Invoke("Инициализация...");
        }
        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.Clear(color: Color.Black);
            Buffer.Graphics.DrawString("ИГРА ОКОНЧЕНА", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold), Brushes.DarkOrange, 20, 200);
            Buffer.Render();
        }
        private static void Form_KeyDown(object sender, KeyEventArgs e) // управление кораблем
        {
            if (e.KeyCode == Keys.ControlKey)
                _bullet.Add(new Bullet(_ship.Rect.Y + 2));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();

            Log?.Invoke($"Кнопка нажата: {e.KeyCode}");
        }
        public static void Timer_tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);

            foreach (BaseObject obj in _game_object) obj?.Draw();

            _ship.Draw();

            foreach (var bullet in _bullet) bullet.Draw();

            // вывод энергии корабля
            if (_ship != null)
                Buffer.Graphics.DrawString("Энергия корабля: " + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 10, 10);
            Buffer.Graphics.DrawString("Очки: " + _points, SystemFonts.DefaultFont, Brushes.White, 10, 30);

            Buffer.Render();
        }
        public static BaseObject[] _game_object;
        public static List<Bullet> _bullet = new List<Bullet>();
        public static Ship _ship;
        public static int _points = 0;
        public static void Load()
        {
            Log?.Invoke("Загрузка сцены >>>");

            var game_object = new List<BaseObject>();

            var rnd = new Random();
            const int asteroids_count = 15;
            const int stars_count = 300;
            const int star_size = 2;
            const int firs_aid_kit_count = 5;

            _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));

            for (int i = 0; i < asteroids_count; i++)
            {
                int r = rnd.Next(5, 50);
                game_object.Add(new Asteroid(new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                    new Point(-r, 0), new Size(r, r)));
            }
            Log?.Invoke($"Астеройдов создано: {asteroids_count}");
            for (int i = 0; i < stars_count; i++)
            {
                int r = rnd.Next(5, 50);
                game_object.Add(new Star(new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                    new Point(-r, 0), new Size(star_size, star_size)));
            }
            Log?.Invoke($"Звезд создано: {stars_count}");
            for (int i = 0; i < firs_aid_kit_count; i++)
            {
                int r = rnd.Next(5, 50);
                game_object.Add(new FirstAidKit(new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                    new Point(15, 0), new Size(15, 15)));
            }
            Log?.Invoke($"Аптечек создано: {firs_aid_kit_count}");

            _game_object = game_object.ToArray();

            Log?.Invoke($"Сцена загружена <<<");
        }
        public static void Update()
        {
            foreach (BaseObject obj in _game_object)
                obj?.Update();

            List<Bullet> remove_bullet = new List<Bullet>();

            foreach (Bullet bullet in _bullet)
            {
                bullet.Update();
                if (bullet.Rect.X > Width)
                    remove_bullet.Add(bullet);
            }
            for (int i = 0; i < _game_object.Length; i++)
            {
                var obj = _game_object[i];
                if (obj is ICollision)
                {
                    var collision_obj = (ICollision)obj;

                    if (_ship.CheckCollision(collision_obj) && (collision_obj is FirstAidKit))
                    {
                        _game_object[i] = null;
                        Log?.Invoke($"Аптечка подобрана");
                    }
                    foreach (Bullet bullet in _bullet.ToArray())
                    {
                        if (bullet.CheckCollision(collision_obj) && (collision_obj is Asteroid))
                        {
                            remove_bullet.Add(bullet);
                            _game_object[i] = null;
                            _points += 10; // за каждый сбитый астеройд дают 10 очков
                            Log?.Invoke($"Астеройд сбит");
                        }
                    }
                }
                if (_ship?.Energy <= 0)
                {
                    Finish();
                    Log?.Invoke($"Корабль уничтожен");
                    break;
                }
            }
            foreach (var bullet in remove_bullet)
                _bullet.Remove(bullet);
        }
    }
}