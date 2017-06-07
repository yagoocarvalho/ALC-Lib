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
        public static Dictionary<string,Functions.Function> allFunctions = new Dictionary<string, Functions.Function>()
        {
            {"Q2"         , FuncQuestion2},
            {"Q3M0"       , FuncQuestion3M0},
            {"Q3M2"       , FuncQuestion3M2},
            {"Q4M0"       , FuncQuestion4M0},
            {"Q4M2"       , FuncQuestion4M2},
            {"Q5"         , FuncQuestion5},
            {"Q6"         , FuncQuestion6},
            {"UserDefined", FunctionExample}
        };

        static void Main (string[] args)
        {
            Console.WriteLine ("Start List 5 Execution");

            // Reading configs
            int    nIntegrationPoints       = Convert.ToInt32 (ConfigurationManager.AppSettings.Get ("nIntegrationPoints"));
            int    questionNumber           = Convert.ToInt32 (ConfigurationManager.AppSettings.Get ("questionNumber"));
            string method                   = ConfigurationManager.AppSettings.Get ("method");
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
            switch (questionNumber)
            {
                case 2:
                    Console.WriteLine ("2)");
                    Console.WriteLine ("I1)");
                    _result = GaussianQuadrature.Solve (allFunctions["Q2"], 0, 1, 2);
                    Console.WriteLine ("Gaussian Result = " + _result);
                    _result = PolinomialIntegration.Solve (allFunctions["Q2"], 0, 1, 10);
                    Console.WriteLine ("Polinomial Result = " + _result);
                    Console.WriteLine ("I2)");
                    _result = GaussianQuadrature.Solve (allFunctions["Q2"], 0, 5, 2);
                    Console.WriteLine ("Gaussian Result = " + _result);
                    _result = PolinomialIntegration.Solve (allFunctions["Q2"], 0, 5, 10);
                    Console.WriteLine ("Polinomial Result = " + _result);
                    Console.Write ("Press any key to continue...");
                    Console.ReadLine ();
                    break;
                case 3:
                    Console.WriteLine ("3)");
                    _result = GaussianQuadrature.Solve (allFunctions["Q3M0"], 0, 10, 3);
                    Console.WriteLine ("Gaussian m0 Result = " + _result);
                    _result = GaussianQuadrature.Solve (allFunctions["Q3M2"], 0, 10, 3);
                    Console.WriteLine ("Gaussian m2 Result = " + _result);
                    _result = PolinomialIntegration.Solve (allFunctions["Q3M0"], 0, 10, 10);
                    Console.WriteLine ("Polinomial m0 Result = " + _result);
                    _result = PolinomialIntegration.Solve (allFunctions["Q3M2"], 0, 10, 10);
                    Console.WriteLine ("Polinomial m2 Result = " + _result);
                    Console.Write ("Press any key to continue...");
                    Console.ReadLine ();
                    break;
                case 4:
                    Console.WriteLine ("4)");
                    _result = GaussianQuadrature.Solve (allFunctions["Q4M0"], 0, 10, 3);
                    Console.WriteLine ("Gaussian m0 Result = " + _result);
                    _result = GaussianQuadrature.Solve (allFunctions["Q4M2"], 0, 10, 3);
                    Console.WriteLine ("Gaussian m2 Result = " + _result);
                    _result = PolinomialIntegration.Solve (allFunctions["Q4M0"], 0, 10, 3);
                    Console.WriteLine ("Polinomial m0 Result = " + _result);
                    _result = PolinomialIntegration.Solve (allFunctions["Q4M2"], 0, 10, 3);
                    Console.WriteLine ("Polinomial m2 Result = " + _result);
                    Console.Write ("Press any key to continue...");
                    Console.ReadLine ();
                    break;
                case 5:
                     Console.WriteLine ("5)");
                    _result = GaussianQuadrature.Solve (allFunctions["Q5"], 0, 4, 2);
                    Console.WriteLine ("Gaussian Result = " + _result);
                    _result = PolinomialIntegration.Solve (allFunctions["Q5"], 0, 4, 3);
                    Console.WriteLine ("Polinomial Result = " + _result);
                    Console.Write ("Press any key to continue...");
                    Console.ReadLine ();
                    break;
                case 6:
                    Console.WriteLine ("6)");
                    _result = IntegrationRules.SolveByMT (allFunctions["Q6"], 0, 3);
                    Console.WriteLine ("Mid Trap -> A(f) = " + _result);
                    Console.WriteLine ("--------------------------------");
                    _result = IntegrationRules.SolveByMTS (allFunctions["Q6"], 0, 3);
                    Console.WriteLine ("Simpson -> A(f) = " + _result);
                    Console.WriteLine ("--------------------------------");
                    _result = GaussianQuadrature.Solve (allFunctions["Q6"], 0, 3, 5);
                    Console.WriteLine ("Using Gauss = " + _result);
                    Console.WriteLine ("--------------------------------");
                    _result = PolinomialIntegration.Solve (allFunctions["Q6"], 0, 3, 5);
                    Console.WriteLine ("Using Polinomial = " + _result);
                    Console.WriteLine ("Finished!");
                    Console.Write ("Press any key to continue...");
                    Console.ReadLine ();
                    break;
                default:
                    Console.WriteLine ("User defined:");
                    _result = GaussianQuadrature.Solve (allFunctions["UserDefined"], inferiorIntegrationLimit, superiorIntegrationLimit, nIntegrationPoints);
                    Console.WriteLine ("Using Gauss = " + _result);
                    _result = PolinomialIntegration.Solve (allFunctions["UserDefined"], inferiorIntegrationLimit, superiorIntegrationLimit, nIntegrationPoints);
                    Console.WriteLine ("Using Polinomial = " + _result);
                    Console.WriteLine ("Finished!");
                    Console.Write ("Press any key to continue...");
                    Console.ReadLine ();
                    break;
            }

        }

        private delegate double Sn (double w);

        /// <summary>
        /// Method that calculates the function to x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static double FunctionExample (double x)
        {
            // Slide's example
            return (Math.Exp(-1.0 * Math.Pow(x, 2.0))); 
        }

        // Checked OK
        private static double SnQ3 (double w)
        {
            // Question 3
            return 2.0;
        }

        // Checked OK
        private static double SnQ4 (double w)
        {
            // Question 4
            double Hs = 3.0;
            double Tz = 5.0;

            double num1 = (4.0 * Math.Pow (Math.PI, 3.0) * Math.Pow (Hs, 2.0));
            double den1 = (Math.Pow (w, 5.0) * Math.Pow (Tz, 4.0));
            double num2 = (16.0 * Math.Pow (Math.PI, 3.0));
            double den2 = (Math.Pow (w, 4.0) * Math.Pow (Tz, 4.0));

            if (w == 0)
            {
                return 0.0;
            }

            double result = (num1/ den1) * (Math.Exp (-1.0 * (num2/ den2)));

            return result;
        } 

        // Checked OK
        private static double Sq (double w, double wn, double e, Sn Sn)
        {
            // Question 3 and 4
            return Math.Pow (RAO (w, wn, e), 2.0) * Sn(w);
        }

        // Checked OK
        private static double RAO (double w, double wn, double e)
        {
            // Questions 3 and 4
            double result =  1.0 / Math.Sqrt (Math.Pow (1.0 - Math.Pow ((w / wn), 2.0), 2.0) + Math.Pow (2.0 * e * (w / wn), 2.0));
            return result;
        }

        // Checked OK
        private static double FuncQuestion2 (double x)
        {
            // Question 2
            return (1.0 / Math.Sqrt (2.0 * Math.PI)) * (Math.Exp ((-1.0 / 2.0) * Math.Pow (x, 2.0)));
        }

        // Checked OK
        private static double FuncQuestion3M0 (double x)
        {
            // Questions 3 and 4
            double w = x;
            double wn = 1.0;
            double e = 0.05;

            // m0
            return Sq(w, wn, e, SnQ3);
        }

        // Checked OK
        private static double FuncQuestion3M2 (double x)
        {
            // Questions 3 and 4
            double w = x;
            double wn = 1.0;
            double e = 0.05;

            //m2
            return Math.Pow (w, 2.0) * Sq (w, wn, e, SnQ3);
        }

        // Checked OK
        private static double FuncQuestion4M0 (double x)
        {
            // Questions 3 and 4
            double w = x;
            double wn = 1.0;
            double e = 0.05;

            // m0
            double result = Sq (w, wn, e, SnQ4);
            return result;
        }

        // Checked OK
        private static double FuncQuestion4M2 (double x)
        {
            // Questions 3 and 4
            double w = x;
            double wn = 1.0;
            double e = 0.05;

            //m2
            return Math.Pow (w, 2.0) * Sq (w, wn, e, SnQ4);
        }

        // Checked OK
        private static double FuncQuestion5 (double x)
        {
            // Question 5
            return 2.0 + (2.0 * x) - (Math.Pow (x, 2.0)) + (3.0 * Math.Pow (x, 3.0));
        }

        // Checked OK
        private static double FuncQuestion6 (double x)
        {
            // Question 6
            return (1.0) / (1.0 + Math.Pow (x, 2.0));
        }
    }
}
