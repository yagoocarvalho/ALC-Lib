using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista6
{
    class RungeKuta4
    {

        public static double f(double t, double x)
        {
            //return (t + x);

            return -2 * t * Math.Pow(x, 2);
        }


        public static void RungeKutaOrdemQuatro(double t_0, double x_0, double h, double t_end)
        {

            double t = t_0, x = x_0;
            Console.WriteLine("T:{0}\tRK4:{1}", t_0, x_0);

            for (t = t_0; t <= t_end; t = t + h)
            {
                x = x + (h / 6) * (K1(t, x) + 2 * K2(t, x, h) + 2 * K3(t, x, h) + K4(t, x, h));
                Console.WriteLine("T:{0}\tRK4:{1}", t + h, x);
            }
        }

        public static double K1(double t, double x)
        {
            return f(t, x);
        }

        public static double K2(double t, double x, double h)
        {
            return f(t + ((0.5) * h), x + (0.5 * K1(t, x) * h));

        }

        public static double K3(double t, double x, double h)
        {
            return f(t + ((0.5) * h), x + (0.5 * K2(t, x, h) * h));

        }

        public static double K4(double t, double x, double h)
        {
            return f(t + h, x + (K3(t, x, h) * h));
        }
    }
}
