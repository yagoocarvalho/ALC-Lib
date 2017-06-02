using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista4
{
    class Program
    {
        private static double _Function (double x)
        {
            double result = 0.0;

            result = Math.Pow (x, 2.0) - 4 * Math.Cos (x);

            return result;
        }

        private static double _result;
        static void Main (string[] args)
        {
            Console.WriteLine ("Start List 4 Execution");

            string stringTol = ConfigurationManager.AppSettings.Get ("tolerance");

            double tol    = Math.Pow (Convert.ToDouble(stringTol.Split('^').First()), Convert.ToDouble(stringTol.Split('^').Last()));
            double a      = Convert.ToDouble (ConfigurationManager.AppSettings.Get ("a"));
            double b      = Convert.ToDouble (ConfigurationManager.AppSettings.Get ("b"));
            string method = ConfigurationManager.AppSettings.Get ("method").ToLower();

            switch (method)
            {
                case "bissection":
                    _result = SolveByBissection (tol, a, b);
                    break;
                case "newton":
                    break;
                default:
                    Console.WriteLine ("Wrong solving method");
                    System.Environment.Exit (-100);
                    break;
            }

            Console.WriteLine ("Final Result = " + _result);

        }

        public static double SolveByBissection(double tol, double a, double b)
        {
            double result = 0.0;
            double x = 0.0;
            double f;
            int count = 0;
            // Bissection method
            while (Math.Abs(b - a) > tol)
            {
                count++;
                x = (a + b) / 2.0;
                f = _Function (x);

                Console.WriteLine (String.Format ("Iter:{0}\ta:{1}\tb:{2}\t:{3}\tf:{4}\t|b-a|:{5} ", count, a, b, x, f, Math.Abs (b - a)));

                if (f > 0.0)
                {
                    b = x;
                }
                else
                {
                    a = x;
                }
            }

            result = x;

            return result;
        }
    }
}
