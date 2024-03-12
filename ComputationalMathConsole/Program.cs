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
            int matrixHeight, matrixWidth;
            float[,] matrixNumbers;
            string? matrixLine = "";
            string[] strNumbers;
            bool IsSuccess = true;

            int numbMethod;

            Console.WriteLine("Решение СЛАУ вида Ax=b. \nВвод матриц вместе со столбцом b");
            Console.Write("Введите высоту матрицы (кол-во строк): ");
            if (!int.TryParse(Console.ReadLine(), out matrixHeight))
            {
                Console.WriteLine("Неправильный ввод числа");
                IsSuccess = false;
            }


            Console.WriteLine();
            Console.Write("Введите ширину матрицы (кол-во столбцов вместе со столбцом b): ");
            if (!int.TryParse(Console.ReadLine(), out matrixWidth))
            {
                Console.WriteLine("Неправильный ввод числа");
                IsSuccess = false;
            }

            if(matrixHeight <2 ||  matrixWidth <3) 
            {
                Console.WriteLine($"Матрицу размера {matrixHeight} на {matrixWidth} программа не посчитает");
                IsSuccess = false;
            }

            if (IsSuccess)
            {
                Console.WriteLine();
                Console.WriteLine("Ввод матрицы (запись десятичных дробей через запятую, разделитель - пробел) : ");

                matrixNumbers = new float[matrixHeight, matrixWidth];
                for (int i = 0; i < matrixHeight; i++)
                {
                    matrixLine = Console.ReadLine();
                    if (matrixLine == string.Empty || matrixLine == null)
                    {
                        Console.WriteLine("Неправильный ввод чисел");
                        return;
                    }
                    else
                    {
                        strNumbers = matrixLine.Split(' ');
                        if (strNumbers.Length == matrixWidth)
                        {
                            for (int k = 0; k < strNumbers.Length; k++)
                            {
                                if (!float.TryParse(strNumbers[k], out matrixNumbers[i, k]))
                                {
                                    Console.WriteLine("Неправильный ввод числа");
                                    return;
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Введено неверное кол-во чисел");
                            return;
                        }
                    }
                }

                Console.WriteLine("\nМЕТОДЫ");
                Console.WriteLine("1 - метод Гаусса без выбора главного элемента");
                Console.WriteLine("2 - метод Гаусса с выбором главного элемента");
                Console.WriteLine("3 - метод простых итераций");
                Console.WriteLine("4 - метод прогонки (для трёхдиагональн. матрицы)");
                Console.Write("\nВведите номер метода: ");

                
                if(!int.TryParse(Console.ReadLine(), out numbMethod) || numbMethod>4 || numbMethod<1)
                {
                    Console.WriteLine("Неверный ввод номера");
                    IsSuccess = false;
                }

                if(IsSuccess)
                { 
                    switch(numbMethod)
                    {
                        case 1:
                            Matrix matrix1 = new(matrixNumbers);
                            GausMethod gausMethod1 = new();
                            Console.WriteLine("Рузультат без выбора главного элемента");
                            PrintResult(gausMethod1.GetResult(matrix1));
                            break;
                        case 2:
                            Matrix matrix2 = new(matrixNumbers);
                            GausMethod gausMethod2 = new();
                            Console.WriteLine("Рузультат с выбором главного элемента");
                            PrintResult(gausMethod2.GetResultMainElement(matrix2));
                            break;
                        case 3:
                            Matrix matrix4 = new(matrixNumbers);
                            SimpleIterationsMethod simpleIterationsMethod = new(matrix4);

                            if (!simpleIterationsMethod.IsRightMatrix())
                                Console.WriteLine("Метод не применим: ноль на главной диагонали");
                            else if (!simpleIterationsMethod.IsIterationsConverge())
                                Console.WriteLine("Метод не применим: не выполнено условие сходимости процесса итерации");
                            else
                            {
                                simpleIterationsMethod.CreateMatrixAlphaBeta();
                                float[] answers = simpleIterationsMethod.Approach();

                                PrintIntermediateValues(matrix4);

                                Console.WriteLine("\nОТВЕТЫ");
                                PrintResult(answers);
                            }
                            break;
                        case 4:
                            Matrix matrix3 = new(matrixNumbers);
                            TridiagonalMatrixAlgorithm tridiagonalMatrixAlgorithm = new(matrix3);

                            Console.WriteLine("Ответы: ");
                            if (tridiagonalMatrixAlgorithm.Answers == null || tridiagonalMatrixAlgorithm.Answers.Length == 0)
                            {
                                Console.WriteLine("Матрица не трёхдиагональная. Метод не применяется");
                                return;
                            }

                            PrintResult(tridiagonalMatrixAlgorithm.Answers);
                            break;
                    }



                    //Matrix m1 = new(new float[3, 4] { { 5, 2.833333f, 1, 11.666666f }, { 3, 1.7f, 7, 13.4f }, { 1, 8, 1, 18 } });
                    
                    //m1 = new(new float[3, 4] { { 5, 2.833333f, 1, 11.666666f }, { 3, 1.7f, 7, 13.4f }, { 1, 8, 1, 18 } });

                    //Matrix matrix = new(matrixNumbers);  //new float[6,7] { { 1, 0, 0, 0, 0, 0, 0 }, {1,-2,1,0,0,0,0.08f } ,{0,1,-2,1,0,0,0.08f } ,{0,0,1,-2,1,0, 0.08f }, {0,0,0,1,-2,1, 0.08f }, {0,0,0,0,0,1,1 }  }
                    
                    //Matrix m2 = new(new float[,] { { 1, 1, 15, 17 }, { 0,15,1, 16}, { 15,4,1,20} });
                    //Matrix m2 = new(new float[,] { { 15, 0, 1, 17 }, { 4, 15, 1, 16 }, { 1, 1, 15, 20 } });
                    
                }
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
