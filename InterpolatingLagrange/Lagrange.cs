using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpolatingLagrange
{
    public class Lagrange
    {
        public double Interpolate(double[,] numbers, double x)
        {
            double result = 0;
            double[] l = new double[numbers.GetLength(1)];
            double li = 1;

            for(int i = 0;i < numbers.GetLength(1); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        li *= (x - numbers[0,j]) / (numbers[0, i] - numbers[0, j]);
                    }
                }
                li *= numbers[1, i];
                l[i] = li;
                li = 1;
            }
            foreach(int i in l)
            {
                result += i;
            }
            return result;
        }
    }
}
