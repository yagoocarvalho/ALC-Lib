using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista5
{
    class Program
    {
        private static double _result;

        static void Main (string[] args)
        {
            Console.WriteLine ("Start List 5 Execution");

            // Reading configs
            int     nIntegrationPoints      = Convert.ToInt32 (ConfigurationManager.AppSettings.Get ("nIntegrationPoints"));
            string  method                  = ConfigurationManager.AppSettings.Get ("method").ToLower ();
            double inferiorIntegrationLimit = Convert.ToDouble (ConfigurationManager.AppSettings.Get ("inferiorIntegrationLimit"));
            double superiorIntegrationLimit = Convert.ToDouble (ConfigurationManager.AppSettings.Get ("superiorIntegrationLimit"));
            
            // Check if the limits are right
            if (inferiorIntegrationLimit > superiorIntegrationLimit)
            {
                Console.WriteLine ("Wrong integration limits");
                Console.Write ("Press any key to continue...");
                Console.ReadKey ();
                System.Environment.Exit (-10);
            }

            // Select the method to solve de function
            switch (method)
            {
                case "polinomial":
                    _result = PolinomialIntegration.Solve (_Function, inferiorIntegrationLimit, superiorIntegrationLimit, nIntegrationPoints);
                    Console.WriteLine ("Final Result = " + _result);
                    Console.Write ("Press any key to continue...");
                    Console.ReadLine ();
                    break;
                case "gaussian-quadrature":
                    _result = GaussianQuadrature.Solve (_Function, inferiorIntegrationLimit, superiorIntegrationLimit, nIntegrationPoints);
                    Console.WriteLine ("Final Result = " + _result);
                    Console.Write ("Press any key to continue...");
                    Console.ReadLine ();
                    break;
                case "rules":
                    _result = IntegrationRules.SolveByMT (_Function, inferiorIntegrationLimit, superiorIntegrationLimit);
                    Console.WriteLine ("A(f) = " + _result);
                    Console.Write ("Press any key to continue...");
                    Console.ReadLine ();
                    _result = IntegrationRules.SolveByMTS (_Function, inferiorIntegrationLimit, superiorIntegrationLimit);
                    Console.WriteLine ("A(f) = " + _result);
                    Console.Write ("Press any key to continue...");
                    Console.ReadLine ();
                    _result = GaussianQuadrature.Solve (_Function, inferiorIntegrationLimit, superiorIntegrationLimit, nIntegrationPoints);
                    Console.WriteLine ("Using Gauss = " + _result);
                    Console.Write ("Press any key to continue...");
                    Console.ReadLine ();
                    _result = PolinomialIntegration.Solve (_Function, inferiorIntegrationLimit, superiorIntegrationLimit, nIntegrationPoints);
                    Console.WriteLine ("Using Polinomial = " + _result);
                    Console.WriteLine ("Finished!");
                    Console.Write ("Press any key to continue...");
                    Console.ReadLine ();
                    break;
                default:
                    Console.WriteLine ("Wrong solving method");
                    Console.Write ("Press any key to continue...");
                    Console.ReadLine ();
                    System.Environment.Exit (-100);
                    break;
            }

        }

        /// <summary>
        /// Method that calculates the function to x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static double _Function (double x)
        {
            // Slide's example
            //return (2.0 + x + (2 * Math.Pow (x, 2.0))); 

            // Question 2
            //return (1.0 / Math.Sqrt (2 * Math.PI)) * Math.Exp ((-1.0 / 2.0) * Math.Pow (x, 2.0));

            // Questions 3 and 4
            //double w = x;
            //double wn = 1.0;
            //double e = 0.05;

            // m0
            //return Sq(w, wn, e);

            //m2
            //return Math.Pow (w, 2.0) * Sq (w, wn, e);

            // Question 5
            //return 2.0 + 2.0 * x - Math.Pow (x, 2.0) + 3 * Math.Pow (x, 3);

            // Question 6
            return (1.0) / (1.0 + Math.Pow (x, 2.0));
        }

        private static double Sn (double w)
        {
            // Question 3
            //return 2.0;

            // Question 4
            double Hs = 3.0;
            double Tz = 5.0;

            return ((4.0 * Math.Pow (Math.PI, 3.0) * Math.Pow (Hs, 2.0)) / (Math.Pow (w, 5.0) * Math.Pow (Tz, 4.0))) * Math.Exp ((-16.0 * Math.Pow (Math.PI, 3.0)) / (Math.Pow (w, 4.0) * Math.Pow (Tz, 4.0)));
        }

        private static double Sq (double w, double wn, double e)
        {
            // Question 3 and 4
            return Math.Pow (RAO (w, wn, e), 2.0) * Sn(w);
        }

        private static double RAO (double w, double wn, double e)
        {
            // Questions 3 and 4
            return 1.0 / Math.Sqrt (Math.Pow (1.0 - Math.Pow ((w / wn), 2.0), 2.0) + Math.Pow (2.0 * e * (w / wn), 2.0));
        }
    }
}
