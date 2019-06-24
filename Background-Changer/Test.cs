using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background_Changer
{
    public class Test
    {
        public void Main()
        {
            var timer = new System.Timers.Timer();

            timer.Interval = 1000;

            timer.Elapsed += (Object source, System.Timers.ElapsedEventArgs e) =>
            {
                //
            };

            timer.Start();

            Console.ReadLine();
        }
    }

  
}
