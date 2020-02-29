using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace AsteroidGame_2020
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        static Game()
        {
        }
        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;

            if((form.Width > 1000 || form.Width < 0) || (form.Height > 1000 || form.Height < 0))
                throw new ArgumentOutOfRangeException("Высота или ширина больше 1000 или принимает отрицательное значение");


            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
                        
            Timer timer = new Timer { Interval = 10 };
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
            Buffer.Graphics.Clear(Color.Black);
            foreach(BaseObject obj in _game_object)
                obj?.Draw();
            _bullet.Draw();
            Buffer.Render();
        }
        public static BaseObject[] _game_object;
        public static Bullet _bullet;
        public static void Load()
        {
            var game_object = new List<BaseObject>();

            var rnd = new Random();            

            const int asteroids_count = 20;
            const int stars_count = 300;
            const int star_size = 2;

            for (int i = 0; i < asteroids_count; i++)
            {
                int r = rnd.Next(5, 50);
                game_object.Add(new Asteroid(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(-r, r), new Size(r, r)));
            }   
            for (int i = 0; i < stars_count; i++)
            {
                int r = rnd.Next(5, 50);
                game_object.Add(new Star(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(-r, r), new Size(star_size, star_size)));
                if (star_size > 2)
                    throw new GameObjectException("Превышен размер звезд");
            }
            _game_object = game_object.ToArray();
            _bullet = new Bullet(200);
        }
        public static void Update()
        {
            foreach (BaseObject obj in _game_object)
                obj?.Update();

            _bullet?.Update();     
            if (_bullet.Position.X > Width)
                _bullet = new Bullet(new Random().Next(Width));

            for (var i = 0; i < _game_object.Length; i++)
            {
                var obj = _game_object[i];
                if (obj is ICollision)
                {
                    ICollision collision_object = obj;
                    if (_bullet.Collision(collision_object))
                    {
                        _bullet = new Bullet(new Random().Next(Width));
                        _game_object[i] = null;
                        System.Media.SystemSounds.Hand.Play();
                    }
                }
            }            
        }
    }
}
