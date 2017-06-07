using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista6
{
    class Program
    {
        
        static void realFunction(double t_0, double t_end, double h)
        {
            double t;
            for(t = t_0; t<=t_end+h; t+=h)
                Console.WriteLine("T:{0}\tReal: {1}", t, 1 / (1 + Math.Pow(t, 2)));
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Runge de Ordem 4");
            RungeKuta4.RungeKutaOrdemQuatro(0, 1, 0.1,2);
            Console.WriteLine("Runge de Ordem 2");
            RungeKuta2.RungeKuttaOrdemDois(0, 1, 0.1,2);
            Console.WriteLine("Euler");
            Euler.EulerMethod(0, 1, 0.01,2);
            Console.WriteLine("Real");
            realFunction(0, 2, 0.1);

            Console.WriteLine ("2a Ordem - Taylor");
            TaylorSeries.Solve (0.0, 1.0, 0.0, 0.0, 0.1, ExampleFunc);

            Console.WriteLine ("RKN");
            RungeKuttaNystrom.Solve (0.0, 1.0, 0.0, 0.0, 0.1, ExampleFunc);

            Console.ReadKey();
        }

        public static double ExampleFunc (double t, double x, double dx)
        {
            return -9.8 - (1.0*dx*Math.Abs(dx));
        }
    }
}
