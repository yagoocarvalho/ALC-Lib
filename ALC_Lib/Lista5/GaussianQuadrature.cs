using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista5
{
    public class GaussianQuadrature
    {
        public delegate double Function (double x);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="f">Function to be integrated</param>
        /// <param name="a">Inferior Integration Limit</param>
        /// <param name="b">Superior Integration Limit</param>
        /// <param name="nIntegrationPoints">Number of integration points</param>
        /// <returns>The result</returns>
        public static double Solve (Function f, double a, double b, int nIntegrationPoints)
        {
            double   result = 0.0;
            double   L      = (b - a);
            double   delta  = L / (nIntegrationPoints - 1);
            double[] points = new double[nIntegrationPoints];

            // Calculate all integration points
            for (int i = 0; i < nIntegrationPoints; i++)
            {
                if ((nIntegrationPoints - 1) != 0)
                    points[i] = a + (delta * i);
                else
                    points[i] = (a + b) / 2;
            }

            return result;
        }
    }
}
