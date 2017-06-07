using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Lista6
{
    class Program
    {
        
        static Dictionary<string,double> realFunction(double t_0, double t_end, double h)
        {
            double t;
            Dictionary<string,double> results = new Dictionary<string, double> ();
            for(t = t_0; t<=t_end+h; t+=h)
            {
                if (h == 0.1)
                {
                    Console.WriteLine("T:{0}\tReal: {1}", t.ToString("0.0"), 1 / (1 + Math.Pow(t, 2)));
                    results.Add (t.ToString ("0.0"), 1 / (1 + Math.Pow (t, 2)));
                }
                else
                {
                    Console.WriteLine ("T:{0}\tReal: {1}", t.ToString ("0.00"), 1 / (1 + Math.Pow (t, 2)));
                    results.Add (t.ToString ("0.00"), 1 / (1 + Math.Pow (t, 2)));
                }
            }
            return results;
                
        }

        static void Main(string[] args)
        {
            double h = 0.01;
            Console.WriteLine("Runge de Ordem 4");
            Dictionary<string,double> RK4Results = RungeKuta4.RungeKutaOrdemQuatro(0, 1, h, 2);
            Console.WriteLine("Runge de Ordem 2");
            Dictionary<string,double> RK2Results = RungeKuta2.RungeKuttaOrdemDois (0, 1, h, 2);
            Console.WriteLine("Euler");
            Dictionary<string,double> EulerResults = Euler.EulerMethod (0, 1, h, 2);
            Console.WriteLine("Real");
            Dictionary<string,double> RealResults = realFunction (0, 2, h);

            //Console.WriteLine ("2a Ordem - Taylor");
            //TaylorSeries.Solve (0.0, 1.0, 0.0, 0.0, 0.1, ExampleFunc);
            //
            //Console.WriteLine ("RKN");
            //RungeKuttaNystrom.Solve (0.0, 1.0, 0.0, 0.0, 0.1, ExampleFunc);

            //Dictionary<string,double> TaylorResult = TaylorSeries.Solve (0.0, 100.0, 0.0, 0.0, 0.01, Ex2);
            //Dictionary<string,double> RKNResult    = RungeKuttaNystrom.Solve (0.0, 100.0, 0.0, 0.0, 0.01, Ex2);

            //using (StreamWriter sw = new StreamWriter (@"C:\Projects\UFRJ\ALC-Lib\ALC_Lib\Lista6\resultEx2p001.csv"))
            //{
            //    sw.WriteLine ("t;Taylor;RKN");
            //    for (double t = 0; t < 100.0; t = t + 0.01)
            //    {
            //        try
            //        {
            //            sw.WriteLine (String.Format ("{0};{1};{2}", t.ToString ("0.00"), TaylorResult[t.ToString ("0.00")], RKNResult[t.ToString ("0.00")]));
            //        }
            //        catch
            //        {
            //            continue;
            //        }
            //    }
            //    sw.Flush ();
            //}

            using (StreamWriter sw = new StreamWriter (@"C:\Projects\UFRJ\ALC-Lib\ALC_Lib\Lista6\resultEx1p001.csv"))
            {
                sw.WriteLine ("t;Euler;RK2;RK4;Real");
                for (double t = 0; t < 100.0; t = t + h)
                {
                    try
                    {
                        if (h == 0.1)
                        {
                            sw.WriteLine (String.Format ("{0};{1};{2};{3};{4}", t.ToString ("0.0"), EulerResults[t.ToString ("0.0")], RK2Results[t.ToString ("0.0")], RK4Results[t.ToString ("0.0")], RealResults[t.ToString ("0.0")]));
                        }
                        else
                        {
                            sw.WriteLine (String.Format ("{0};{1};{2};{3};{4}", t.ToString ("0.00"), EulerResults[t.ToString ("0.00")], RK2Results[t.ToString ("0.00")], RK4Results[t.ToString ("0.00")], RealResults[t.ToString ("0.00")]));
                        }

                    }
                    catch
                    {
                        continue;
                    }
                }
                sw.Flush ();
            }

            Console.ReadKey ();
        }

        public static double ExampleFunc (double t, double x, double dx)
        {
            return -9.8 - (1.0*dx*Math.Abs(dx));
        }

        public static double Ex2 (double t, double x, double dx)
        {
            double m = 1.0;
            double c = 0.2;
            double k = 1.0;

            return (Ex2F(t) - k*x - c*dx)/m;
        }

        public static double Ex2F (double t)
        {
            double w = 0.5;
            return 2*Math.Sin(w*t) + Math.Sin(2*w*t) + Math.Cos(3*w*t);
        }
    }
}
