namespace InterpolatingLagrange;
public class Program
{
    static void Main()
    {
        Lagrange l = new();
        double[,] numbers = new double[,] { { 2, 3, 5 }, { 6, 4, -6 } };
        Console.WriteLine(l.Interpolate(numbers, 5));
    }
}

