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
        /// <summary>
        /// Method that calculates the function to x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static double _Function (double x)
        {
            double result = 0.0;
            string functionString = ConfigurationManager.AppSettings.Get ("function");
            // TODO: CHANGE THIS TO THE FUNCTION WANTED FUNCTION
            result = Math.Pow (x, 2.0) - 4 * Math.Cos (x);       // Slide's example
            //result = Math.Pow (x, 2.0) - 4 * Math.Cos (x);

            return result;
        }

        /// <summary>
        /// Method that calculates the function to x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static double _FunctionDerivative (double x)
        {
            double result = 0.0;
            string functionString = ConfigurationManager.AppSettings.Get ("function");
            // TODO: CHANGE THIS TO THE FUNCTION WANTED FUNCTION
            result = (2 * x) + (4 * Math.Sin(x));       // Slide's example
            //result = Math.Pow (x, 2.0) - 4 * Math.Cos (x);

            return result;
        }

        private static double _result;

        static void Main (string[] args)
        {
            Console.WriteLine ("Start List 4 Execution");

            string stringTol = ConfigurationManager.AppSettings.Get ("tolerance");
            string stringDX  = ConfigurationManager.AppSettings.Get ("dx");

            double tol    = Convert.ToDouble(stringTol.Split('^').First().Split('*').First()) * 
                            Math.Pow (Convert.ToDouble(stringTol.Split('^').First().Split('*').Last()), 
                                      Convert.ToDouble(stringTol.Split('^').Last())
                            );
            double dx     = Convert.ToDouble (stringDX.Split ('^').First ().Split ('*').First ()) *
                            Math.Pow (Convert.ToDouble (stringDX.Split ('^').First ().Split ('*').Last ()),
                                      Convert.ToDouble (stringDX.Split ('^').Last ())
                            );
            double a      = Convert.ToDouble (ConfigurationManager.AppSettings.Get ("a"));
            double b      = Convert.ToDouble (ConfigurationManager.AppSettings.Get ("b"));
            double x0     = Convert.ToDouble (ConfigurationManager.AppSettings.Get ("x0"));
            int    nIter  = Convert.ToInt32  (ConfigurationManager.AppSettings.Get ("nIter"));


            string method = ConfigurationManager.AppSettings.Get ("method").ToLower();

            switch (method)
            {
                case "bissection":
                    _result = SolveByBissection (tol, a, b);
                    break;
                case "newton-original":
                    _result = SolveByNewtonOriginal (tol, x0, nIter);
                    break;
                case "newton-secant":
                    _result = SolveByNewtonSecant (tol, x0, nIter, dx);
                    break;
                default:
                    Console.WriteLine ("Wrong solving method");
                    System.Environment.Exit (-100);
                    break;
            }

            if (_result == -1)
            {
                System.Environment.Exit (-101);
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

        public static double SolveByNewtonOriginal (double tol, double x0, double nIter)
        {
            double       result  = 0.0;
            double       tolk    = 0.0;
            List<double> x       = new List<double>();
            x.Add (x0);

            for (int k = 1; k <= nIter; k++)
            {
                x.Add (x[k-1] - ( _Function (x[k-1]) / _FunctionDerivative (x[k-1]) ) );
                tolk = Math.Abs (x[k] - x[k-1]);
                Console.WriteLine (String.Format ("Iter:{0}\tx[k-1]:{1}\tf(x[k-1]):{2}\tf'(x[k-1]):{3}\tx[k]:{4}\ttolk:{5} ", 
                                                    k, 
                                                    x[k - 1].ToString("0.000"),
                                                    _Function (x[k - 1]).ToString ("0.000"),
                                                    _FunctionDerivative (x[k - 1]).ToString ("0.000"),
                                                    x[k].ToString ("0.000"), 
                                                    tolk));
                if (tolk < tol)
                {
                    result = x[k];
                    return result;
                }
            }

            return -1;
        }

        public static double SolveByNewtonSecant (double tol, double x0, double nIter, double dx)
        {
            double       result  = 0.0;
            double       tolk    = 0.0;
            List<double> x       = new List<double> ();
            x.Add (x0);
            x.Add (x0 + dx);

            double fa = _Function (x0);
            double fi = 0.0;

            for (int k = 1; k <= nIter; k++)
            {
                fi = _Function (x[k]);
                x.Add (x[k] - (fi * (x[k] - x[k-1]) / (fi - fa)));
                tolk = Math.Abs (x[k+1] - x[k]);
                Console.WriteLine (String.Format ("Iter:{0}\tx[k-1]:{1}\tx[k]:{2}\tx[k+1]:{3}\ttolk:{4}",
                                                    k,
                                                    x[k - 1].ToString ("0.000"),
                                                    x[k].ToString ("0.000"),
                                                    x[k + 1].ToString ("0.000"),
                                                    tolk));
                if (tolk < tol)
                {
                    result = x[k];
                    return result;
                }
                else
                {
                    fa = fi;
                }
            }

            return -1;
        }
    }
}
