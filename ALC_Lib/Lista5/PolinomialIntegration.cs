using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista5
{
    public class PolinomialIntegration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="f">Function to be integrated</param>
        /// <param name="a">Inferior Integration Limit</param>
        /// <param name="b">Superior Integration Limit</param>
        /// <param name="nIntegrationPoints">Number of integration points</param>
        /// <returns>The result</returns>
        public static double Solve (Functions.Function f, double a, double b, int nIntegrationPoints)
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

            double[] weights = CalculateWeights (points, a, b);
            for (int i = 0; i < nIntegrationPoints; i++)
            {
                result += weights[i] * f (points[i]);
            }

            return result;
        }

        private static double[] CalculateWeights (double[] points, double infLim, double supLim)
        {
            double[]  b           = new double[points.Length];
            double[]  weights     = new double[points.Length];
            double[,] vandermonde = new double[points.Length, points.Length];

            for (int j = 0; j < points.Length; j++)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    vandermonde[i, j] = Math.Pow (points[j], i);
                }

                b[j] = (Math.Pow (supLim, j + 1) - Math.Pow (infLim, j + 1)) / (j + 1);
            }

            // Initialize VandermondeMatrix and bVector to calculate the weights
            Matrix<double> vandermondeMatrix = DenseMatrix.OfArray (vandermonde);
            Vector<double> bVector           = DenseVector.OfArray (b);

            weights = (vandermondeMatrix.Inverse () * bVector).AsArray ();

            return weights;
        }
    }
}
