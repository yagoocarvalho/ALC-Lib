using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista5
{
    public class IntegrationRules
    {
        public delegate double Function (double x);

        public static double MidPointRule (Function f, double a, double b)
        {
            double m = (b + a) / 2.0;
            double L = (b - a);

            return f (m) * L;
        }

        public static double TrapeziumRule (Function f, double a, double b)
        {
            double L = (b - a);
            return ((f(a) + f(b)) / 2.0) * L;
        }

        public static double SimpsonRule (Function f, double a, double b)
        {
            double m = (b + a) / 2.0;
            double L = (b - a);

            return ((f (a) + 4 * f (m) + f (b))) * (L / 6.0);
        }

        public static double SolveByMT (Function f, double a, double b)
        {
            Console.WriteLine ("Using MidPoint and Trapezium");
            double Mf = MidPointRule  (f, a, b);
            double Tf = TrapeziumRule (f, a, b);
            double Ef = (Tf - Mf) / 3.0;
            Console.WriteLine ("Mf = " + Mf);
            Console.WriteLine ("Tf = " + Tf);
            Console.WriteLine ("Ef = " + Ef);

            return Mf + Ef;
        }

        public static double SolveByMTS (Function f, double a, double b)
        {
            Console.WriteLine ("Using Simpson");
            double Mf = MidPointRule  (f, a, b);
            double Tf = TrapeziumRule (f, a, b);
            double Sf = SimpsonRule   (f, a, b);
            double Ef = (Tf - Mf) / 3.0;
            double Ff = (Tf - Mf - 3 * Ef) / 5.0;
            Console.WriteLine ("Mf = " + Mf);
            Console.WriteLine ("Tf = " + Tf);
            Console.WriteLine ("Sf = " + Sf);
            Console.WriteLine ("Ef = " + Ef);
            Console.WriteLine ("Ff = " + Ff);

            return Sf - (2*Ff/3);
        }
    }
}
