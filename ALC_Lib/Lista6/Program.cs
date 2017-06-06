using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista6
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("Runge de Ordem 4");
            RungeKuta4.RungeKutaOrdemQuatro(0, 0, 0.1);
            Console.WriteLine("Runge de Ordem 2");
            RungeKuta2.RungeKuttaOrdemDois(0, 0, 0.1);
            Console.WriteLine("Euler");
            Euler.EulerMethod(0, 0, 0.1);
            Console.ReadKey();
        }
    }
}
