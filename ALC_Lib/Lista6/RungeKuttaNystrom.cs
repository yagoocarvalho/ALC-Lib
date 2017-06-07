using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista6
{
    public class RungeKuttaNystrom
    {
        public delegate double Function (double t, double x, double dx);

        public static void Solve (double t_0, double t_end, double x_0, double dx_0, double h, Function f)
        {
            double t = 0.0,
                   x = x_0,
                   dx = dx_0,
                   k1, k2, k3, k4, q, l;
            Console.WriteLine ("T:{0}\tRKN:{1}", t_0, x_0.ToString ("0.000"));
            for (t = t_0; t <= t_end; t = t + h)
            {
                k1 = K1 (h, t, x, dx, f);
                q  = Q (h, dx, k1);
                k2 = K2 (h, t, x, dx, k1, q, f);
                k3 = K3 (h, t, x, dx, k2, q, f);
                l  = L (h, dx, k3);
                k4 = K4 (h, t, x, dx, k3, l, f);
                x  = x + h*(dx + (1.0/3.0)*(k1 + k2 + k3));
                dx = dx + (1.0 / 3.0)*(k1 + 2*k2 + 2*k3 + k4);
                Console.WriteLine ("T: {0}\tRKN: {1}", (t + h), x.ToString ("0.000"));
            }
        }

        public static double K1 (double h, double t, double x, double dx, Function f)
        {
            return ((1.0 / 2.0)*h) * f (t, x, dx);
        }

        public static double K2 (double h, double t, double x, double dx, double K1, double Q, Function f)
        {
            return ((1.0 / 2.0) * h) * f (t + (h/2.0), x + Q, dx + K1);
        }

        public static double K3 (double h, double t, double x, double dx, double K2, double Q, Function f)
        {
            return ((1.0 / 2.0)*h) * f (t + (h / 2.0), x + Q, dx + K2);
        }

        public static double K4 (double h, double t, double x, double dx, double K3, double L, Function f)
        {
            return ((1.0 / 2.0)*h) * f (t + h, x + L, dx + (2*K3));
        }

        public static double Q (double h, double dx, double K1)
        {
            return ((1.0 / 2.0)*h) * (dx + (1.0 / 2.0)*K1);
        }

        public static double L (double h, double dx, double K3)
        {
            return h*(dx + K3);
        }
    }
}
