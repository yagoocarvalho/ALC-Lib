using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista6
{
    class RungeKuta2
    {

        public static double f(double t, double x)
        {
            return (t + x);
        }

        //Implementação da função 
        public static void RungeKuttaOrdemDois(double t_0, double x_0, double h)
        {

            double t = 0, x = x_0;

            Console.WriteLine("T:{0}\tRK2:{1}", t_0, x_0);


            for (t =t_0; t < 1.0-h; t = t + h)
            {
                x = x + (h / 2) * (K1(t, x) + K2(t, x, h) );
                Console.WriteLine("T:{0}\tRK2:{1}", t+h, x);
            }
        }


        public static double K1(double t, double x)
        {
            return f(t, x);
        }

        public static double K2(double t, double x, double h)
        {
            return f(t + h, x + (K1(t, x) * h));

        }


    }
}
