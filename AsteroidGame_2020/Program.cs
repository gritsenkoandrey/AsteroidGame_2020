﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGame_2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            form.Width = 800;
            form.Height = 600;

            form.Show();

            Game.Init(form);
            Game.Load();
            Game.Draw();

            Application.Run(form);
        }
    }
}
