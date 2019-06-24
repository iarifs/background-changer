using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Background_Changer
{

    public sealed class Wallpaper
    {
        Wallpaper() { }

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public enum Style : int
        {
            Tiled,
            Centered,
            Stretched,
            Fill
        }

        public static void Set(Style style)
        {
            const string HOST = "https://api.unsplash.com/photos/random?client_id=8dc68398339d6a94c531f9971a3204a50c08013c95ff13ac0edf41061b6e9c46";

            WebClient wc = new WebClient();

            HttpClient httpClient = new HttpClient();

            var response = httpClient.GetAsync(HOST).Result;

            var data = response.Content.ReadAsStringAsync();

            dynamic ParsedObject = JsonConvert.DeserializeObject(data.Result);

            var url = $"{ParsedObject["urls"]["raw"]}";

            Console.WriteLine("*******************");
            Console.WriteLine("Getting the data from unsplash");

            byte[] bytes = wc.DownloadData(url);
            MemoryStream ms = new MemoryStream(bytes);

            Console.WriteLine("*******************");
            Console.WriteLine("Parsing the image from link");

            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
            img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

            Console.WriteLine("*******************");
            Console.WriteLine("Saving image on system");

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            if (style == Style.Stretched)
            {
                key.SetValue(@"WallpaperStyle", 2.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (style == Style.Centered)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (style == Style.Tiled)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 1.ToString());
            }
            if (style == Style.Fill)
            {
                key.SetValue(@"WallpaperStyle", 10.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }
            Console.WriteLine("*******************");
            Console.WriteLine("Set image as desktop wallpaper");

            SystemParametersInfo(SPI_SETDESKWALLPAPER,
                0,
                tempPath,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            Console.WriteLine("**************");
            Console.WriteLine("*****************");
            Console.WriteLine("***********");
            Console.WriteLine("*********");
            Console.WriteLine("***************");
            Console.WriteLine("Press S key to skip the wallpaper");
        }
    }
}
