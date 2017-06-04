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
        /// <summary>
        /// Method that calculates the function to x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static double _Function (double x)
        {
            return Math.Exp (-1 * Math.Pow (x, 2.0));
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
            result = (2 * x) + (4 * Math.Sin (x));       // Slide's example
            //result = Math.Pow (x, 2.0) - 4 * Math.Cos (x);

            return result;
        }

        private static double _result;

        static void Main (string[] args)
        {
            Console.WriteLine ("Start List 4 Execution");

            // Reading configs
            int     nIntegrationPoints       = Convert.ToInt32 (ConfigurationManager.AppSettings.Get ("nIntegrationPoints"));
            string  method                   = ConfigurationManager.AppSettings.Get ("method").ToLower ();
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
                case "gauss-quadrature":
                    _result = GaussianQuadrature.Solve (_Function, inferiorIntegrationLimit, superiorIntegrationLimit, nIntegrationPoints);
                    Console.WriteLine ("Final Result = " + _result);
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

        private static double SolveByGaussQuadrature ()
        {
            throw new NotImplementedException ();
        }
        
    }
}
