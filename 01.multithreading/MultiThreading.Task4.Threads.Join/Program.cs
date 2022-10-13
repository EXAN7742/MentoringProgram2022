﻿/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static Semaphore semaphor = new Semaphore(10, 10);
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            // feel free to add your code
           
            //PartA(10);
            PartB(10);
            

            Console.ReadLine();
        }

        private static void PartA(object num)
        {
            int curNum = (int)num;            
            if (curNum > 0)
            {  
                Console.WriteLine($"{Thread.CurrentThread.Name} - {curNum}");
                curNum--;
                Thread thread = new Thread(PartA);
                thread.Name = $"Thread {curNum}";
                thread.Start(curNum);
                thread.Join();

                Console.WriteLine($"Ended work with {curNum}");
            }
        }

        private static void PartB(object num)
        {
            int curNum = (int)num;
            if (curNum > 0)
            {
               
                Console.WriteLine($"{Thread.CurrentThread.Name} - {curNum}");
                curNum--;
                ThreadPool.QueueUserWorkItem(new WaitCallback(PartB), curNum);
                Thread.Sleep(2000);

                semaphor.WaitOne();
                Console.WriteLine($"Ended work with {curNum}");
                semaphor.Release();
            }
        }
    }
}
