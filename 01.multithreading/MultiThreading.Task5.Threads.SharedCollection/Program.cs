/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static Random random = new Random();
        const int size = 10;
        static Semaphore semaphor = new Semaphore(1,1);

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            List<int> numbers = new List<int>();
            int printedNumbers = 0;
            
            Task taskAdd = new Task(() =>
            {
                for(int i = 0; i < size; i++)
                {
                    semaphor.WaitOne();
                    numbers.Add(random.Next(1,10));
                    Thread.Sleep(1000);  //Почему не хочет работать без sleep
                    semaphor.Release();
                }
                
            });
            
            Task taskPrint = new Task(() =>
            {
                while (printedNumbers <= numbers.Count)
                {
                    semaphor.WaitOne();
                    foreach (int num in numbers)
                    {
                        Console.Write($"{num} ");
                    }
                    printedNumbers++;
                    Console.WriteLine();
                    Thread.Sleep(1000);
                    semaphor.Release();
                } 
            });

            taskPrint.Start();
            taskAdd.Start();

            Task.WaitAll(taskAdd, taskPrint);
            Console.ReadLine();
        }
    }
}
