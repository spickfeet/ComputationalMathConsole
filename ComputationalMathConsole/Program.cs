namespace ComputationalMathConsole;

class Program
{
    static void Main()
    {       
        Matrix m1 = new(new float[3, 4] { { 5, 2.833333f, 1, 11.666666f }, { 3, 1.7f, 7, 13.4f }, { 1, 8, 1, 18 } });
        GausMethod g = new();

        Console.WriteLine("Рузультат без выбора главного элемента");
        PrintResult(g.GetResult(m1));

        m1 = new(new float[3, 4] { { 5, 2.833333f, 1, 11.666666f }, { 3, 1.7f, 7, 13.4f }, { 1, 8, 1, 18 } });

        Console.WriteLine("Рузультат с выбором главного элемента");
        PrintResult(g.GetResultMainElement(m1));
    }
    static private void PrintResult(float[] results)
    {
        for (int i = 0; i < results.Length; i++)
        {
            Console.WriteLine($"x{i + 1} = {results[i]} ");
        }
    }
}