using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalMathConsole
{
    class Program
    {
        static void Main()
        {
            //Matrix m1 = new(new float[3, 4] { { 5, 2.833333f, 1, 11.666666f }, { 3, 1.7f, 7, 13.4f }, { 1, 8, 1, 18 } });
            //GausMethod g = new();

            //Console.WriteLine("Рузультат без выбора главного элемента");
            //PrintResult(g.GetResult(m1));

            //m1 = new(new float[3, 4] { { 5, 2.833333f, 1, 11.666666f }, { 3, 1.7f, 7, 13.4f }, { 1, 8, 1, 18 } });

            //Console.WriteLine("Рузультат с выбором главного элемента");
            //PrintResult(g.GetResultMainElement(m1));


            //Matrix m2 = new(new float[,] { { 1, 1, 15, 17 }, { 0,15,1, 16}, { 15,4,1,20} });
            Matrix m2 = new(new float[,] { { 15,0,1,17 }, { 4,15,1,16}, { 1,1,15,20} });
            SimpleIterationsMethod simpleIterationsMethod = new(m2);

            if (!simpleIterationsMethod.IsRightMatrix())
                Console.WriteLine("Метод не применим: ноль на главной диагонали");
            else if (!simpleIterationsMethod.IsIterationsConverge())
                Console.WriteLine("Метод не применим: не выполнено условие сходимости процесса итерации");
            else
            {
                simpleIterationsMethod.CreateMatrixAlphaBeta();
                float[] answers = simpleIterationsMethod.Approach();

                PrintIntermediateValues(m2);

                Console.WriteLine("\nОТВЕТЫ");
                for (int i = 0; i < answers.Length; i++)
                    Console.WriteLine("х"+(i+1) + ": " + answers[i]);

            }
        }
        static private void PrintResult(float[] results)
        {
            for (int i = 0; i < results.Length; i++)
            {
                Console.WriteLine($"x{i + 1} = {results[i]} ");
            }
        }

        static private void PrintIntermediateValues(Matrix matrix)
        {
            Console.WriteLine("Промежуточные результаты итераций\n");
            for (int i=0;i< SimpleIterationsMethod.answersList.Count;i++)
            {
                Console.Write(SimpleIterationsMethod.answersList[i] + " ");
                if ((i + 1) % (matrix.Width - 1) == 0)
                    Console.WriteLine("\n");
            }
        }
    }
}
