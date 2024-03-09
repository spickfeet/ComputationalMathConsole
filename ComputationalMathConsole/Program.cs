namespace ComputationalMathConsole;


class GausMethod
{
    private Matrix _matrix;
    public Matrix Matrix
    {
        get { return _matrix; }
        set { _matrix = value; }
    }
    public GausMethod(Matrix matrix)
    {
        Matrix = matrix;
    }
    public void Direct()
    {
        int n = 0;
        for (int i = 0; i < Matrix.Height - 1; i++)
        {
            for (int a = n + 1; a < Matrix.Height; a++)
            {
                float coef = Matrix[a, i] / Matrix[n, i];

                for (int w = 0; w < Matrix.Width; w++)
                {
                    Matrix[a, w] -= Matrix[n, w] * coef;
                }
            }
            Matrix.Print();
            if (n < Matrix.Height)
            {
                n++;
            }
            else break;
        }
    }
    public void Indirect()
    {
        int n = Matrix.Height - 1;
        for (int i = Matrix.Width - 2; i > -1; i--)
        {
            for (int a = n - 1; a > -1; a--)
            {
                float coef = Matrix[a, i] / Matrix[n, i];
                for (int w = Matrix.Width - 1; w > -1; w--)
                {
                    Matrix[a, w] -= Matrix[n, w] * coef;
                }
            }
            if (n > 1)
            {
                n--;
            }
            else break;
        }
    }

    public float[] GiveAnswers()
    {
        float[] answers = new float[Matrix.Height];
        for (int i = 0; i < Matrix.Height; i++)
        {
            float coef = 0;
            for (int j = 0; j < Matrix.Width - 1; j++)
            {
                if (Matrix[i, j] != 0)
                {
                    coef = Matrix[i, j];
                }
            }

            answers[i] = Matrix[i, Matrix.Width - 1] / coef;
        }

        return answers;
    }
}

class Program
{
    static void Main()
    {
        Matrix m1 = new(new float[3, 4] { { 5, 2.833333f, 1, 11.666666f }, { 3, 2.833333f, 7, 11.666666f }, { 1, 8, 1, 18 } });

        //m1.Print();
        //GausMethod g = new GausMethod();
        //g.GetMatrix(m1);
        //g.Direct();
        //m1 = g.matrix;
        //g.Indirect();
        ////m1 = g.matrix;
        //m1.Print();
        //Console.WriteLine();
        //Console.WriteLine();
        //Console.WriteLine("Ответы: ");

        //float[] answers = g.GiveAnswers();
        //foreach (var ans in answers)
        //{
        //    Console.Write($"{ans} ");
        //}
        ////Console.WriteLine();
    }
}