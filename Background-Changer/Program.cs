using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using Microsoft.Win32;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace Background_Changer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Intitalize Apps......");


            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(5);

            var timer = new System.Threading.Timer((e) =>
            {
                Wallpaper.Set(Wallpaper.Style.Fill);
            }, null, startTimeSpan, periodTimeSpan);


            while (true)
            {
                var input = Console.ReadLine();
                Wallpaper.Set(Wallpaper.Style.Fill);
            }

        }

    }

}

