

using System;

namespace Spline
{
    public class TridiagonalMatrixAlgorithm    //метод прогонки
    {
        private Matrix _matrix;
        private int _maxMatrixIndex;   //максимальный индекс матрицы (если индексация строк начинается с 0)

        private double[] _massive_e;
        private double[] _massive_d;
        private double[] _massive_c;
        private double[] _massive_b;
        private double[] _massive_alpha;
        private double[] _massive_beta;

        private double[] _answers;
        public Matrix Matrix
        {
            get => _matrix;
            set => _matrix = value;
        }

        public double[] Answers
        {
            get => _answers;
            private set => _answers = value;
        }

        public TridiagonalMatrixAlgorithm(Matrix matrix)
        {
            Matrix = matrix;
            if (IsTridiagonalMatrix())
            {
                
                _maxMatrixIndex = Matrix.Height - 1;

                SetValueMassive_e();
                SetValueMassive_d();
                SetValueMassive_c();
                SetValueMassive_b();

                _massive_alpha = new double[Matrix.Height];
                _massive_alpha[0] = 0;

                _massive_beta = new double[Matrix.Height];
                _massive_beta[0] = 0;

                Direct();
                Indirect();
            }
            
                
        }

        private bool IsTridiagonalMatrix()
        {
            if (Matrix.Width*Matrix.Height < 9)
                return false;
            bool isNull = true;
            for(int i = 0; i < Matrix.Height-2; i++) 
            {
                for (int j = 2+i; j < Matrix.Width-1; j++)
                {
                    if (Matrix[i, j] != 0)
                    {
                        isNull = false;
                        return isNull;
                    }

                    if (!isNull)
                        return isNull;
                }
            }

            for (int i = 2; i < Matrix.Height-1; i++)
            {
                for (int j = 0; j < i-1; j++)
                {
                    if (Matrix[i, j] != 0)
                    {
                        isNull = false;
                        return isNull;
                    }

                    if (!isNull)
                        return isNull;
                }
            }

            return true;
        }

        private void SetValueMassive_e()
        {
            _massive_e = new double[Matrix.Height];
            _massive_e[_maxMatrixIndex] = 0;

            for(int index=0;index<_maxMatrixIndex;index++) 
                _massive_e[index] = Matrix[index,index+1];
            
        }
        private void SetValueMassive_d()
        {
            _massive_d = new double[Matrix.Height];
            for (int index = 0;index<_maxMatrixIndex+1;index++)
                _massive_d[index] = Matrix[index,index];
        }
        private void SetValueMassive_c()
        {
            _massive_c = new double[Matrix.Height];
            _massive_c[0] = 0;

            for(int index = 1; index<_maxMatrixIndex+1; index++)
                _massive_c[index] = Matrix[index, index-1];
        }
        private void SetValueMassive_b()
        {
            _massive_b = new double[Matrix.Height];
            for (int index = 0;index<_maxMatrixIndex + 1;index++)
                _massive_b[index] = Matrix[index,Matrix.Width-1];
        }

        private void Direct()    //прямой ход
        {
            _massive_alpha[1] = _massive_e[0] / _massive_d[0];
            _massive_beta[1] = _massive_b[0] / _massive_d[0];

            for(int index = 2;index<_maxMatrixIndex+1;index++)
            {
                _massive_alpha[index] = _massive_e[index-1] / (_massive_d[index-1] - _massive_c[index - 1] * _massive_alpha[index-1]);
                _massive_beta[index] = (_massive_b[index - 1] - _massive_c[index - 1] * _massive_beta[index-1]) / (_massive_d[index - 1] - _massive_c[index - 1] * _massive_alpha[index - 1]);
            }
        }
        private void Indirect()   //обратный ход
        {
            Answers = new double[Matrix.Height];
            Answers[_maxMatrixIndex] = (_massive_b[_maxMatrixIndex] - _massive_c[_maxMatrixIndex] * _massive_beta[_maxMatrixIndex]) / (_massive_d[_maxMatrixIndex] - _massive_c[_maxMatrixIndex] * _massive_alpha[_maxMatrixIndex]);
            for (int index = _maxMatrixIndex - 1; index >= 0; index--)
                Answers[index] = _massive_beta[index+1] - _massive_alpha[index + 1] * Answers[index+1];
        }

    }
}
