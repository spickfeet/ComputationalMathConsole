
using Spline;

CubeSpline spline = new();
double[,] numbers = new double[2, 4] { { 1, 2, 4, 7},{ 2, 3, 1, 4} };
double[,] splineSystem = spline.GetSplineSystem(numbers);