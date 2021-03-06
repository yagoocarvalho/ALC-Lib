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

        public static Dictionary<string, double> EulerMethod (double t_0, double x_0, double h, double t_end)
        {
            double t = 0, x = x_0;
            Dictionary<string,double> results = new Dictionary<string, double> ();
            for (t = t_0; t <= t_end; t = t + h)
            {
                if (h == 0.1)
                {
                    Console.WriteLine("T:{0}\tEuler:{1}", t.ToString("0.0"), x);
                    results.Add (t.ToString ("0.0"), x);
                }
                else
                {
                    Console.WriteLine ("T:{0}\tEuler:{1}", t.ToString("0.00"), x);
                    results.Add (t.ToString ("0.00"), x);
                }
                x = x+ (K1(t, x) *h);
            }

            return results;
        }



        public static double K1(double t, double x)
        {
            return f(t, x);
        }

       


    }
}
