/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        const int arraySize = 10;
        const int mult = 5;
        static readonly Random random = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            //1
            Task<int[]> task1 = Task.Run(() =>
            {
                int[] array = new int[arraySize];
                for (int i = 0; i < arraySize; i++)
                {
                    array[i] = random.Next(1, 10);
                }
                Print(1, array);
                return array;
            });

            //2
            Task<int[]> task2 = task1.ContinueWith(x =>
            {
                for (int i = 0; i < arraySize; i++)
                {
                    task1.Result[i] *= mult;
                }
                Print(2, task1.Result);
                return task1.Result;
            });

            //3
            Task<int[]> task3 = task2.ContinueWith(x =>
            {
                Array.Sort(task2.Result);

                Print(3, task2.Result);
                return task2.Result;
            });

            //4
            Task task4 = task3.ContinueWith(x =>
            {
                Console.WriteLine("Task {0}", 4);
                Console.WriteLine($"Average: {task3.Result.ToList().Average()}");
            });


            Console.ReadLine();
        }

        private static void Print(int taskNumber, int[] array)
        {
            Console.WriteLine("Task {0}", taskNumber);
            foreach (int item in array)
                Console.Write($"{item} ");
            Console.WriteLine();
        }
    }
}
