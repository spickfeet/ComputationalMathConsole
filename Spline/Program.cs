
using Spline;

CubeSpline spline = new();
double[,] numbers = new double[2, 4] { { 1, 2, 4, 7},{ 2, 3, 1, 4} };
double[,] matrixA = new double[4*(numbers.GetLength(1) - 1), 4 * (numbers.GetLength(1) - 1)];
double[] matrixB = new double[4 * (numbers.GetLength(1) - 1)];
spline.FirstCondition(matrixA, matrixB, numbers);