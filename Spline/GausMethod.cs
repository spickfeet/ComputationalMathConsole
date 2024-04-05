using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spline
{
    /// <summary>
    /// Класс для расчета методом Гаусса 
    /// </summary>
    public class GausMethod
    {
        /// <summary>
        /// Поиск индекса строки с максимальным значением в стообце
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private int GetLineIndexWithMaxElementAbs(int columnIndex, Matrix matrix)
        {
            double maxValue = 0;
            int lineIndex = -1;
            for (int j = columnIndex; j < matrix.Height; j++)
            {
                if (maxValue < Math.Abs(matrix[j, columnIndex]))
                {
                    maxValue = Math.Abs(matrix[j, columnIndex]);
                    lineIndex = j;
                }
            }
            return lineIndex;
        }

        /// <summary>
        /// Перестановка строк
        /// </summary>
        /// <param name="lineIndex1"></param>
        /// <param name="lineIndex2"></param>
        /// <param name="matrix"></param>
        private void SwapLine(int lineIndex1,int lineIndex2, Matrix matrix)
        {
            double temp;
            for (int j = 0; j < matrix.Width; j++)
            {
                temp = matrix[lineIndex1, j];
                matrix[lineIndex1, j] = matrix[lineIndex2, j];
                matrix[lineIndex2, j] = temp;
            }
        }

        /// <summary>
        /// Прямой ход с выбором главного элемента
        /// </summary>
        /// <param name="matrix"></param>
        private void DirectMainElement(Matrix matrix)
        {
            for (int i = 0; i < matrix.Height - 1; i++)
            {
                SwapLine(i, GetLineIndexWithMaxElementAbs(i, matrix), matrix);
                for (int a = i + 1; a < matrix.Height; a++)
                {
                    double coef = matrix[a, i] / matrix[i, i];

                    for (int w = 0; w < matrix.Width; w++)
                    {
                        matrix[a, w] -= matrix[i, w] * coef;
                    }
                }
            }
        }

        /// <summary>
        /// Прямой ход без выбора главного элемента
        /// </summary>
        /// <param name="matrix"></param>
        private void Direct(Matrix matrix)
        {
            for (int i = 0; i < matrix.Height - 1; i++)
            {

                for (int a = i + 1; a < matrix.Height; a++)
                {
                    double coef = matrix[a, i] / matrix[i, i];

                    for (int w = 0; w < matrix.Width; w++)
                    {
                        matrix[a, w] -= matrix[i, w] * coef;
                    }
                }
            }
        }

        /// <summary>
        /// Обратный ход
        /// </summary>
        /// <param name="matrix"></param>
        private double[] InDirect(Matrix matrix)
        {
            double[] results = new double[matrix.Height];
            results[matrix.Height - 1] = matrix[matrix.Height - 1, matrix.Width - 1] / matrix[matrix.Height - 1, matrix.Width - 2];
            for (int i = matrix.Height - 2; i > -1;i--)
            {
                double sum = 0;
                for (int j = i + 1; j < matrix.Width - 1; j++)
                {
                    sum += matrix[i, j] * results[j];
                }
                results[i] = (matrix[i, matrix.Width - 1] - sum)/ matrix[i, i];
            }
            return results;
        }

        /// <summary>
        /// Получить рузультат без выбора главного элемента
        /// </summary>
        /// <param name="matrix"></param>
        public double[] GetResult(Matrix matrix)
        {
            Direct(matrix);
            return InDirect(matrix);
        }

        /// <summary>
        /// Получить рузультат с выбором главного элемента
        /// </summary>
        /// <param name="matrix"></param>
        public double[] GetResultMainElement(Matrix matrix)
        {
            DirectMainElement(matrix);
            return InDirect(matrix);
        }
    }
}
