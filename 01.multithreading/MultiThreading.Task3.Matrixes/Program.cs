/*
 * 3. Write a program, which multiplies two matrices and uses class Parallel.
 * a. Implement logic of MatricesMultiplierParallel.cs
 *    Make sure that all the tests within MultiThreading.Task3.MatrixMultiplier.Tests.csproj run successfully.
 * b. Create a test inside MultiThreading.Task3.MatrixMultiplier.Tests.csproj to check which multiplier runs faster.
 *    Find out the size which makes parallel multiplication more effective than the regular one.
 */

using System;
using System.Diagnostics;
using MultiThreading.Task3.MatrixMultiplier.Matrices;
using MultiThreading.Task3.MatrixMultiplier.Multipliers;

namespace MultiThreading.Task3.MatrixMultiplier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("3.	Write a program, which multiplies two matrices and uses class Parallel. ");
            Console.WriteLine();

            const int matrixSize = 400; // todo: use any number you like or enter from console
            CreateAndProcessMatrices(matrixSize);
            Console.ReadLine();
        }

        private static void CreateAndProcessMatrices(int sizeOfMatrix)
        {
            Console.WriteLine("Multiplying...");
            var firstMatrix = new Matrix(sizeOfMatrix, sizeOfMatrix, true);
            var secondMatrix = new Matrix(sizeOfMatrix, sizeOfMatrix, true);

            Double timeResult = 0;
            Double timeResultParallel = 0;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            IMatrix resultMatrix = new MatricesMultiplier().Multiply(firstMatrix, secondMatrix);
            stopWatch.Stop();
            timeResult = stopWatch.ElapsedMilliseconds;

            stopWatch.Reset();
            stopWatch.Start();
            IMatrix resultMatrixParallel = new MatricesMultiplierParallel().Multiply(firstMatrix, secondMatrix);
            stopWatch.Stop();
            timeResultParallel = stopWatch.ElapsedMilliseconds;

            Console.WriteLine("firstMatrix:");
            firstMatrix.Print();
            Console.WriteLine("secondMatrix:");
            secondMatrix.Print();

            Console.WriteLine();
            Console.WriteLine($"result Matrix: {timeResult} ms");
            resultMatrix.Print();

            Console.WriteLine($"resultMatrix parallel: {timeResultParallel} ms");
            resultMatrixParallel.Print();
        }
    }
}
