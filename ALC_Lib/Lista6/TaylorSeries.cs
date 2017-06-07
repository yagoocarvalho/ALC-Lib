using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista6
{
    public class TaylorSeries
    {
        public delegate double Function (double t, double x, double dx);

        public static Dictionary<string, double> Solve (double t_0, double t_end, double x_0, double dx_0, double h, Function f)
        {
            double t = 0,
                   x = x_0,
                   dx = dx_0,
                   d2x;
            Dictionary<string,double> results = new Dictionary<string, double> ();
            for (t = t_0; t <= t_end; t = t + h)
            {
                Console.WriteLine ("T:{0}\tTaylor:{1}", t.ToString("0.00"), x.ToString("0.0000"));
                results.Add (t.ToString("0.00"), x);

                d2x = f (t, x, dx);
                x = x + dx * h + (1.0/2.0) * d2x * Math.Pow(h, 2.0);
                dx = dx + d2x * h;
            }

            return results;
        }
    }
}
