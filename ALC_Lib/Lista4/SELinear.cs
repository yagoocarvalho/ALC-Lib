using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;



namespace Lista4
{
    static class Constants
    {
        public const double teta1 = 0;
        public const double teta2 = 3;

    }
    class SELinear
    {
        //Functions 
        static double f1(double[] x)
        {

            //Exercício 3
            //return 16 * Math.Pow(x[0], 4) + 16 * Math.Pow(x[1], 4) + Math.Pow(x[2], 4) - 16;

            //Exercício 4
            return 2 * Math.Pow(x[1], 2) +  Math.Pow(x[0], 2) + 6*Math.Pow(x[2], 2) - 1;

            //Exercício 5
            //return x[0] + x[1] * Math.Pow(1, x[2]) - 1;

        }
        static double f2(double[] x)
        {
            //Exercício 3
            //return Math.Pow(x[0], 2) + Math.Pow(x[1], 2) + Math.Pow(x[2], 2) - 3;

            //Exercício 4
            return 8 * Math.Pow(x[1], 3) +
                6*x[1]*Math.Pow(x[0], 2) + 36 *x[0]*x[1]*x[2]+108*x[1]*Math.Pow(x[2],2) - Constants.teta1;

            //Exercício 5
            //return x[0] + x[1] * Math.Pow(2, x[2]) - 2;

        }
        static double f3(double[] x)
        {
            //Exercício 3
            //return Math.Pow(x[0],3) - x[1] + x[2] - 1;

            //Exercício 4
            return 60 * Math.Pow(x[1], 4) + 60 * Math.Pow(x[1], 2) * Math.Pow(x[0], 2) +
                   576 * Math.Pow(x[1], 2) * x[0] * x[2] + 2232 * Math.Pow(x[1], 2) * Math.Pow(x[2], 2) +
                   252 * Math.Pow(x[2], 2) * Math.Pow(x[0], 2) + 1296 * Math.Pow(x[2], 3) * x[0] +
                   3348 * Math.Pow(x[2], 4) + 24 * Math.Pow(x[0], 3) * x[2] + 3 * x[0] - Constants.teta2;

            //Exercício 5
            //return x[0] + x[1] * Math.Pow(3, x[2]) - 9;
        }

        static Matrix<double> fMatrix(Matrix<double> x)
        {
            Matrix<Double> fMatrix = DenseMatrix.OfArray(
                            new double[,] { { f1(x.ToColumnMajorArray()) },
                                            { f2(x.ToColumnMajorArray()) },
                                            { f3(x.ToColumnMajorArray()) }});
            return fMatrix;
        }

        // Gradient of the functions.
        static double[] df1(double[] x)
        {
            double[] df = new double[x.Count()];
            //Exercício 3
            //df[0] = 4 * 16 * Math.Pow(x[0], 3);
            //df[1] = 2*x[0];
            //df[2] = 3 * Math.Pow(x[0], 2);

            //Exercício 4
            df[0] = 2 * x[0];
            df[1] = 12 * x[1] * x[0] + 36 * x[1] * x[2];
            df[2] = 60 * 2 * x[0] * Math.Pow(x[1], 2) + 576 * Math.Pow(x[1], 2) * x[2] +
                252 * 2 * Math.Pow(x[2], 2) * x[0] + 1296 * Math.Pow(x[2], 3) + 3 * 24 * Math.Pow(x[0], 2) * x[2] + 3;

            //Exercício 5
            //df[0] = 1;
            //df[1] = 1;
            //df[2] = 1;


            return df;
        }
        static double[] df2(double[] x)
        {
            double[] df = new double[x.Count()];
            //Exercício 3
            //df[0] = 4 * 16 * Math.Pow(x[1], 3);
            //df[1] = 2 * x[1];
            //df[2] = -1;


            //Exercício 4
            df[0] = 2 * 2 * x[1];
            df[1] = 24 * Math.Pow(x[1], 2) + 6 * Math.Pow(x[0], 2) + 36 * x[0] * x[2] + 108 * Math.Pow(x[2], 2);
            df[2] = 4 * 60 * Math.Pow(x[1], 3) + 60 * 2 * x[1] * Math.Pow(x[0], 2) + 2 * 576 * x[0] * x[1] * x[2] +
                2232 * 2 * x[1] * Math.Pow(x[2], 2);

            //Exercício 5
            //df[0] = Math.Pow(1,x[2]) ;
            //df[1] = Math.Pow(2, x[2]);
            //df[1] = Math.Pow(3, x[2]);
            return df;
        }
        static double[] df3(double[] x)
        {
            double[] df = new double[x.Count()];
            //Exercício 3
            //df[0] = 4 * Math.Pow(x[2], 3);
            //df[1] = 2 * x[2];
            //df[2] = 1;

            //Exercício 4
            df[0] = 2 * 6 * x[2];
            df[1] = 36 * x[1] * x[0] + 108 * 2 * x[1] * x[2];
            df[2] = 576 * Math.Pow(x[1], 2) * x[0] + 2232*2*Math.Pow(x[1],2)*x[2] + 
                252 * 2 * x[2] * Math.Pow(x[0], 2) + 1296*3*Math.Pow(x[2],2)*x[0]+
                3348*4*Math.Pow(x[2],3) + 24*Math.Pow(x[0],3);

            //Exercício 5
            //df[0] = 0;
            //df[1] = Math.Pow(2, x[2]) * x[1] * Math.Log(2);
            //df[2] = Math.Pow(3, x[2]) * x[1] *Math.Log(3);

            return df;
        }


        public static void SolveSEByBroyden(double tol, double x1, double x2, double x3)
        {
            double tolk = 0.0;
            Matrix<double> result;
            Matrix<Double> jacobian, deltaX, solution, Y, B;
            //initialGuess
            Matrix<double> x0 = DenseMatrix.OfArray(new Double[,] { { x1 }, { x2 }, { x3 } });

            double[] diff1 = df1(x0.ToColumnMajorArray());
            double[] diff2 = df2(x0.ToColumnMajorArray());
            double[] diff3 = df3(x0.ToColumnMajorArray());
            jacobian = DenseMatrix.OfArray(new Double[,]
                { { diff1[0], diff2[0], diff3[0] },
                      { diff1[1], diff2[1], diff3[1] },
                      { diff1[2], diff2[2], diff3[2] },
                  });

            for (int i = 1; i < 1000; i++)
            {

                deltaX = -jacobian.Inverse().Multiply(fMatrix(x0));

                solution = deltaX.Add(x0);
                Y = fMatrix(solution).Subtract(fMatrix(x0));

                Console.WriteLine(String.Format("Iter:{0}\tx1:{1}\tx2:{2}\tx3:{3}",
                                                                i, solution[0, 0].ToString("0.0000000"),
                                                                solution[1, 0].ToString("0.0000000"),
                                                                solution[2, 0].ToString("0.0000000")));
                tolk = deltaX.FrobeniusNorm() / solution.FrobeniusNorm();
                if (tolk < tol)
                {
                    result = solution;
                    Console.WriteLine(String.Format("Result: X1:{0}\tX2:{1}\tx3:{2}",
                                                                solution[0, 0].ToString("0.000"),
                                                                solution[1, 0].ToString("0.000"),
                                                                solution[2, 0].ToString("0.000")));
                    return;
                }
                else
                {
                    B = jacobian.Add((Y.Subtract(jacobian.Multiply(deltaX))).Multiply(deltaX.Transpose())
                                      .Multiply(((deltaX.Transpose().Multiply(deltaX)).Inverse())[0, 0]));
                    jacobian = B;
                    x0 = solution;
                }
            }

        }

        public static void SolveSEByNewton(double tol, double x1, double x2, double x3)
        {
            double tolk = 0.0;
            Matrix<double> result;
            Matrix<Double> jacobian, deltaX, solution;
            //initialGuess
            Matrix<double> x0 = DenseMatrix.OfArray(new Double[,] { { x1 }, { x2 } , { x3 } });
            Console.WriteLine(x0.ToColumnMajorArray()[2]);
            for (int i = 1; i < 1000; i++)
            {
                double[] diff1 = df1(x0.ToColumnMajorArray());
                double[] diff2 = df2(x0.ToColumnMajorArray());
                double[] diff3 = df3(x0.ToColumnMajorArray());
                jacobian = DenseMatrix.OfArray(new Double[,] 
                    { { diff1[0], diff2[0], diff3[0] },
                      { diff1[1], diff2[1], diff3[1] },
                      { diff1[2], diff2[2], diff3[2] },
                      });

                deltaX = -jacobian.Inverse().Multiply(fMatrix(x0));
                solution = deltaX.Add(x0);
                Console.WriteLine(String.Format("Iter:{0}\tx1:{1}\tx2:{2}\tx3:{3}",
                                                                i, solution[0, 0].ToString("0.000"),
                                                                solution[1, 0].ToString("0.000"),
                                                                solution[2, 0].ToString("0.000")));
                tolk = deltaX.FrobeniusNorm() / solution.FrobeniusNorm();
                Console.WriteLine("Tol: {0}", tolk);
                if (tolk < tol)
                {
                    result = solution;
                    Console.WriteLine(String.Format("Result: X1:{0}\tX2:{1}\tx3:{2}",
                                                                solution[0, 0].ToString("0.000"),
                                                                solution[1, 0].ToString("0.000"),
                                                                solution[2, 0].ToString("0.000")));
                    return;
                }
                else
                {
                    x0 = DenseMatrix.OfArray(new Double[,] { { solution[0, 0] }, { solution[1, 0] }, { solution[2, 0] } });
                }
            }
        }
        public static void SolveLeastSquare(double tol, double x1, double x2, double x3)
        {
            double tolk = 0.0;
            Matrix<double> result;
            Matrix<Double> jacobian, deltaB, solution;
            //initialGuess
            Matrix<double> x0 = DenseMatrix.OfArray(new Double[,] { { x1 }, { x2 }, { x3 } });
            Console.WriteLine(x0.ToColumnMajorArray()[2]);
            for (int i = 1; i < 1000; i++)
            {
                double[] diff1 = df1(x0.ToColumnMajorArray());
                double[] diff2 = df2(x0.ToColumnMajorArray());
                double[] diff3 = df3(x0.ToColumnMajorArray());

                jacobian = DenseMatrix.OfArray(new Double[,]
                    { { diff1[0], diff2[0], diff3[0] },
                      { diff1[1], diff2[1], diff3[1] },
                      { diff1[2], diff2[2], diff3[2] },
                      });

                deltaB = -(((jacobian.Transpose().Multiply(jacobian))).Inverse()).
                    Multiply( jacobian.Transpose().Multiply(fMatrix(x0)));
                solution = deltaB.Add(x0);
                Console.WriteLine(String.Format("Iter:{0}\tx1:{1}\tx2:{2}\tx3:{3}",
                                                                i, solution[0, 0].ToString("0.000"),
                                                                solution[1, 0].ToString("0.000"),
                                                                solution[2, 0].ToString("0.000")));
                tolk = deltaB.FrobeniusNorm() / solution.FrobeniusNorm();
                Console.WriteLine("Tol: {0}", tolk);
                if (tolk < tol)
                {
                    result = solution;
                    Console.WriteLine(String.Format("Result: X1:{0}\tX2:{1}\tx3:{2}",
                                                                solution[0, 0].ToString("0.000"),
                                                                solution[1, 0].ToString("0.000"),
                                                                solution[2, 0].ToString("0.000")));
                    return;
                }
                else
                {
                    x0 = DenseMatrix.OfArray(new Double[,] { { solution[0, 0] }, { solution[1, 0] }, { solution[2, 0] } });
                }
            }
        }
    }
}
