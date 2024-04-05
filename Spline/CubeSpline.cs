using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Spline
{
    internal class CubeSpline
    {

        public void GetCoefA(double[] a, double[,] numbers)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = numbers[1,i];
            }
        }
        public void GetCoefC(out double[] c, double[,] numbers)
        {
            int rowIndex = 0;
            int columnIndex = 0;
            double[,] matrixC = new double[numbers.GetLength(1) - 1, numbers.GetLength(1)];
            matrixC[rowIndex, 0] = 1;
            rowIndex++;

            for (int i = 1; i < numbers.GetLength(1) - 1; i++)
            {
                matrixC[rowIndex, i - 1] = numbers[0,i] - numbers[0, i - 1];
                matrixC[rowIndex, i] = 2 * (numbers[0, i] - numbers[0, i - 1] + numbers[0, i + 1] - numbers[0, i]);

                if(i + 1 != numbers.GetLength(1))
                    matrixC[rowIndex, i + 1] = numbers[0, i + 1] - numbers[0, i];


                matrixC[rowIndex, matrixC.GetLength(1) - 1] = 3 * ((numbers[1,i + 1] - numbers[1,i])/(numbers[0, i + 1] - numbers[0, i]) -
                    (numbers[1, i] - numbers[1, i - 1]) / (numbers[0, i] - numbers[0, i - 1]));
                rowIndex++;
            }
            Matrix matrix = new(matrixC);
            GausMethod g = new GausMethod();
            c = g.GetResultMainElement(matrix); 
        }

        public void GetCoefD(double[] d, double[] c, double[,] numbers)
        {
            for (int i = 0; i < numbers.GetLength(1) - 1; i++)
            {
                if (i + 1 == numbers.GetLength(1) - 1)
                {
                    d[i] = (0 - c[i]) / (3 * (numbers[0, i + 1] - numbers[0, i]));
                    continue;
                }
                d[i] = (c[i + 1] - c[i]) / (3 * (numbers[0, i + 1] - numbers[0, i]));
            }
        }
        public void GetCoefB(double[] b, double[] c, double[,] numbers)
        {
            for (int i = 0; i < numbers.GetLength(1) - 1; i++)
            {
                if (i + 1 == numbers.GetLength(1) - 1)
                {
                    b[i] = (numbers[1, i + 1] - numbers[1, i]) / (numbers[0, i + 1] - numbers[0, i]) - ((0 + 2 * c[i]) * (numbers[0, i + 1] - numbers[0, i])) / 3;
                    continue;
                }
                b[i] = (numbers[1,i + 1] - numbers[1, i]) / (numbers[0,i + 1] - numbers[0, i]) - ((c[i+1] + 2 * c[i]) * (numbers[0,i + 1] - numbers[0, i])) /3;
            }
        }

        public double[,] GetSplineSystem(double[,] numbers)
        {
            double[] a = new double[numbers.GetLength(1) - 1];
            double[] b = new double[numbers.GetLength(1) - 1];
            double[] c = new double[numbers.GetLength(1) - 1];
            double[] d = new double[numbers.GetLength(1) - 1];

            GetCoefA(a, numbers);
            GetCoefC(out c, numbers);
            GetCoefB(b, c, numbers);
            GetCoefD(d, c, numbers);
   
            double[,] result = new double[a.Length, 4];
            for (int i = 0; i < a.Length; i++)
            {
                result[i, 0] = a[i];
                result[i, 1] = b[i];
                result[i, 2] = c[i];
                result[i, 3] = d[i];
            }
            return result;
        }
        public double Interpolate(double[,] numbers, double[,] splineSystem, double x) 
        {
            int splineIndex = numbers.GetLength(1) - 1;
            for(int i = numbers.GetLength(1) - 2; i > 0;i--)
            {
                if (numbers[0,i] > x)
                {
                    splineIndex = i - 1;
                }
            }
            return splineSystem[splineIndex, 0] + splineSystem[splineIndex, 1] * x + splineSystem[splineIndex, 2] * Math.Pow(x,2) + splineSystem[splineIndex,3] * Math.Pow(x, 3);

        }



        //public void FirstCondition(double[,] matrixA, double[] matrixB, double[,] numbers)
        //{
        //    int rowIndex = 0;
        //    int columnIndex = 0;
        //    for (int i = 0; i < numbers.GetLength(1) - 1; i++)
        //    {
        //        matrixA[rowIndex, columnIndex] = 1;
        //        matrixB[rowIndex] = numbers[1, i];

        //        rowIndex++;
        //        matrixB[rowIndex] = numbers[1, i + 1];

        //        matrixA[rowIndex, columnIndex] = 1;
        //        for (int j = 1; j < 4; j++)
        //        {
        //            matrixA[rowIndex, columnIndex + j] = Math.Pow(numbers[0, i + 1] - numbers[0, i], j);
        //        }
        //        rowIndex++;
        //        columnIndex += 4;

        //    }

        //}
        //public void SecondCondition(double[,] matrixA, double[] matrixB, double[,] numbers)
        //{
        //    int rowIndex = (numbers.GetLength(1) - 1) * 2;
        //    int columnIndex = 0;
        //    for (int i = 1; i < numbers.GetLength(1) - 1; i++)
        //    {
        //        matrixA[rowIndex, columnIndex + 1] = 1;
        //        matrixA[rowIndex, columnIndex + 2] = 2 * (numbers[0, i] - numbers[0, i - 1]);
        //        matrixA[rowIndex, columnIndex + 3] = 3 * Math.Pow(numbers[0, i] - numbers[0, i - 1], 2);
        //        matrixA[rowIndex, columnIndex + 5] = -1;
        //        matrixB[rowIndex] = 0;

        //        rowIndex++;

        //        matrixA[rowIndex, columnIndex + 2] = 1;
        //        matrixA[rowIndex, columnIndex + 3] = 3 * (numbers[0, i] - numbers[0, i - 1]);
        //        matrixA[rowIndex, columnIndex + 6] = -1;
        //        rowIndex++;
        //        columnIndex += 4;
        //    }
        //}
        //public void ThirdCondition(double[,] matrixA, double[] matrixB, double[,] numbers)
        //{
        //    int rowIndex = matrixA.GetLength(1) - 1;
        //    matrixA[rowIndex, matrixA.GetLength(1) - 2] = 2;
        //    matrixA[rowIndex, matrixA.GetLength(1) - 1] = 6 * (numbers[0, numbers.GetLength(1) - 1] - numbers[0, numbers.GetLength(1) - 2]);
        //}
        //public void FineCoef(double[,] matrixA, double[] matrixB, double[,] numbers)
        //{
        //    FirstCondition(matrixA, matrixB, numbers);
        //    SecondCondition(matrixA, matrixB, numbers);
        //    ThirdCondition(matrixA, matrixB, numbers);
        //}
    }
}
