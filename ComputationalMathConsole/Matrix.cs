using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalMathConsole
{
    public class Matrix
    {
        private int _height;
        private int _width;
        private float[,] _numbers;

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public float[,] Numbers
        {
            get { return _numbers; }
            set
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

        public Matrix(float[,] numbers)
        {
            Height = numbers.GetLength(0);
            Width = numbers.GetLength(1);
            _numbers = new float[_height, _width];
            Numbers = numbers;
        }
        public float this[int i, int j]
        {
            get => Numbers[i, j];
            set => Numbers[i, j] = value;
        }
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
        //public static Matrix operator +(Matrix m1, Matrix m2)
        //{
        //    if (m1._height != m2._height || m1._width != m2._width)
        //    {
        //        throw new ArgumentException("Нельзя складывать матрицы разного размера");
        //    }
        //    for (int i = 0; i < m1._height; i++)
        //    {
        //        for (int j = 0; j < m1._width; j++)
        //        {
        //            m1[i, j] += m2[i, j];
        //        }
        //    }
        //    return m1;
        //}
        //public static Matrix operator *(Matrix m1, Matrix m2)
        //{
        //    Matrix result = new Matrix(m1._height, m1._width);
        //    for (int i = 0; i < m1._height; i++)
        //    {
        //        for (int j = 0; j < m1._width; j++)
        //        {

        //        }
        //    }
        //    return m1;
        //}
    }
}
