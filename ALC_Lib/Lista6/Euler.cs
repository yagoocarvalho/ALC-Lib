﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista6
{
    class Euler
    {
        public static double f(double t, double x)
        {
            //return (t + x);
            return -2 * t * Math.Pow(x, 2);
        }

        public static void EulerMethod(double t_0, double x_0, double h, double t_end)
        {

            double t = 0, x = x_0;
            Console.WriteLine("T:{0}\tEuler:{1}", t_0, x_0);
            for (t = t_0; t <= t_end; t = t + h)
            {
                x = x+ (K1(t, x) *h);
                Console.WriteLine("T:{0}\tEuler:{1}", t+h, x);
            }
        }



        public static double K1(double t, double x)
        {
            return f(t, x);
        }

       


    }
}
