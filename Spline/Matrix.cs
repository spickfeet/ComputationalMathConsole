using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spline
{
    /// <summary>
    /// Класс с матрицей
    /// </summary>
    public class Matrix
    {
        private int _height;
        private int _width;
        private double[,] _numbers;

        public int Height
        {
            get { return _height; }
            private set { _height = value; }
        }

        public int Width
        {
            get { return _width; }
            private set { _width = value; }
        }
        public double[,] Numbers
        {
            get { return _numbers; }
            private set
            {
                for (int i = 0; i < value.GetLength(0); i++)
                {
                    for (int j = 0; j < value.GetLength(1); j++)
                    {
                        _numbers[i, j] = value[i, j];
                    }
                }
            }
        }

        public Matrix(double[,] numbers)
        {
            Height = numbers.GetLength(0);
            Width = numbers.GetLength(1);
            _numbers = new double[_height, _width];
            Numbers = numbers;
        }
        public double this[int i, int j]
        {
            get => Numbers[i, j];
            set => Numbers[i, j] = value;
        }

        /// <summary>
        /// Выводит матрицу в консоль
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Console.Write($"{_numbers[i, j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }        

        
    }
}
