using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;


namespace Lista4
{
    class NonlinearEquations
    {

        // First set of functions.
        static double f1(double[] x)
        {
            //return x[0] + 2*x[1] -2;
            return Math.Pow(x[0], 2) + x[0] * x[1] - 10;
        }
        static double f2(double[] x)
        {
            //return Math.Pow(x[0], 2) + 4 * Math.Pow(x[1], 2) - 4; 
            return x[1] + 3 * x[0] * Math.Pow(x[1], 2) - 57;
        }

        static Matrix<double> fMatrix(Matrix<double> x) {
            Matrix<Double> fMatrix = DenseMatrix.OfArray( new double[,] { { f1(x.ToColumnMajorArray()) }, { f2(x.ToColumnMajorArray()) }  } );
            return fMatrix;
        }

        // Gradient of the first set of functions.
        static double[] df1(double[] x)
        {
            double[] df = new double[x.Count()];
            //df[0] = 1;
            //df[1] = 2*x[0];
            df[0] = 2 * x[0] + x[1];
            df[1] = 3 * Math.Pow(x[1],2);
            return df;
        }
        static double[] df2(double[] x)
        {
            double[] df = new double[x.Count()];
            //df[0] = 2;
            //df[1] = 8*x[1];
            df[0] = x[0];
            df[1] = 1 + 6 * x[1] * x[0];
            return df;
        }


        public static void SolveSEByBroyden(double tol, double x1, double x2) {
            double tolk = 0.0;
            Matrix<double> result;
            Matrix<Double> jacobian, deltaX, solution, Y, B, M;
            //initialGuess
            Matrix<double> x0 = DenseMatrix.OfArray(new Double[,] { { x1 }, { x2 } });

            double[] diff1 = df1(x0.ToColumnMajorArray());
            double[] diff2 = df2(x0.ToColumnMajorArray());
            jacobian = DenseMatrix.OfArray(new Double[,] { { diff1[0], diff2[0] }, { diff1[1], diff2[1] } });

            for (int i = 1; i < 20; i++)
            {

                Console.WriteLine("X0: {0} e F(x0) {1}", x0, fMatrix(x0));
                deltaX = -jacobian.Inverse().Multiply(fMatrix(x0));
                Console.WriteLine("Deltax: {0}", deltaX);

                solution = deltaX.Add(x0);
                Y = fMatrix(solution).Subtract(fMatrix(x0));
                Console.WriteLine("Y::::");
                Console.WriteLine(Y);

                Console.WriteLine(String.Format("Iter:{0}\tx1:{1}\tx2:{2}", 
                                                                i, solution[0, 0].ToString("0.0000000"), 
                                                                solution[1, 0].ToString("0.0000000")));
                tolk = deltaX.FrobeniusNorm() / solution.FrobeniusNorm();
                Console.WriteLine("tolk : {0}", tolk);
                Console.WriteLine("X:");
                Console.WriteLine(solution);
                if (tolk < tol)
                {
                    result = solution;
                    Console.WriteLine(String.Format("Result: X1:{0}\tX2:{1}", 
                                                                solution[0, 0].ToString("0.000"),
                                                                solution[1, 0].ToString("0.000")));
                    return;
                }
                else
                {
                    B = jacobian.Add((Y.Subtract(jacobian.Multiply(deltaX))).Multiply(deltaX.Transpose())
                                      .Multiply(((deltaX.Transpose().Multiply(deltaX)).Inverse())[0, 0]));
                    jacobian = B;
                    x0 = solution;
                    Console.WriteLine(B);
                }
            }

        }

        public static void SolveSEByNewton(double tol, double x1, double x2)
        {
            double tolk = 0.0;
            Matrix<double> result;
            Matrix<Double> jacobian, deltaX, solution;
            //initialGuess
            Matrix<double> x0 = DenseMatrix.OfArray(new Double[,] { { x1 }, { x2 } });

            for (int i = 1; i < 20; i++)
            {
                double[] diff1 = df1(x0.ToColumnMajorArray());
                double[] diff2 = df2(x0.ToColumnMajorArray());
                jacobian = DenseMatrix.OfArray(new Double[,] { { diff1[0], diff2[0] }, { diff1[1], diff2[1] } });

                deltaX = -jacobian.Inverse().Multiply(fMatrix(x0));
                solution = deltaX.Add(x0);
                Console.WriteLine(String.Format("Iter:{0}\tx1:{1}\tx2:{2}",
                                                                i, solution[0, 0].ToString("0.000"),
                                                                solution[1, 0].ToString("0.000")));
                tolk = deltaX.FrobeniusNorm() / solution.FrobeniusNorm();
                Console.WriteLine("Tol: {0}", tolk);
                if (tolk < tol)
                {
                    result = solution;
                    Console.WriteLine(String.Format("Result: X1:{0}\tX2:{1}",
                                                                solution[0, 0].ToString("0.000"),
                                                                solution[1, 0].ToString("0.000")));
                    return;
                }
                else
                {
                    x0 = DenseMatrix.OfArray(new Double[,] { { solution[0, 0] }, { solution[1, 0] } });
                }
            }

        }

        static void Main3(string[] args)
        {

            double tolk = 0.0;
            Matrix<double> result;
            string stringTol = ConfigurationManager.AppSettings.Get("tolerance");
            double tol = Convert.ToDouble(stringTol.Split('^').First().Split('*').First()) *
                      Math.Pow(Convert.ToDouble(stringTol.Split('^').First().Split('*').Last()),
                                Convert.ToDouble(stringTol.Split('^').Last())
                      );
            double x1 = 1;
            double x2 = 3;
            SolveSEByNewton(tol, x1, x2);
            Console.ReadLine();

        }

    }



}
