using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpolatingLagrange
{
    public class Lagrange
    {
        public double Interpolate(double[][] numbers, double x)
        {
            double result =0;

            for(int i = 0;i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length; j++)
                {
                    if (i == j)
                    {
                        result = (x - numbers[0][j])/(numbers[0][i] - numbers[0][j])* numbers[1][i];

                    }
                }
            }
            return result;
        }
    }
}
