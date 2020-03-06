using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace AsteroidGame_2020
{
    class Program
    {
        static void Main()
        {
            Game.Log += log_str => Debug.WriteLine($">>>{log_str}");
            
            //const string data_txt = "data.txt";
            //File.Create("data.txt");

            Form form = new Form
            {
                Width = 800,
                Height = 600
            };

            Game.Init(form);
            form.Show();
            Game.Load();
            Game.Draw();

            Application.Run(form);
        }
    }
}
