using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spline
{
    internal class CubeSpline
    {
        
        public void FirstCondition(double[,] matrixA, double[] matrixB, double[,] numbers)
        {
            int rowIndex = 0;
            int columnIndex = 0;
            for (int i = 0; i < numbers.GetLength(1) - 1; i++)
            {
                matrixA[rowIndex, columnIndex] = 1;
                matrixB[rowIndex] = numbers[1, i];
                for (int j = 1; j < 4; j++)
                {
                    matrixA[rowIndex, columnIndex + j] = 0;
                }

                rowIndex++;
                matrixB[rowIndex] = numbers[1, i + 1];

                matrixA[rowIndex, columnIndex] = 1;
                for (int j = 1;j < 4; j++)
                {                    
                    matrixA[rowIndex, columnIndex + j] = Math.Pow(numbers[0, i + 1] - numbers[0, i], j);
                }
                rowIndex++;
                columnIndex+=4;

            }
        }
    }
}
