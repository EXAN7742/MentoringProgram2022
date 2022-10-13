/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            // feel free to add your code

            //PartA();
            //PartB();
            //PartC();
            PartD();

            Console.ReadLine();
        }

        private static void PartD()
        {
            Task<bool> task1 = Task.Run(() =>
            {
                Thread.CurrentThread.Name = "My thread";
                Console.WriteLine($"Executing task 1: {Thread.CurrentThread.Name}");
                Thread.Sleep(1000);
                return true;
            });
            Task task2 = task1.ContinueWith(x =>
            {
                Console.WriteLine($"Executing task 2: {Thread.CurrentThread.Name}");
                Thread.Sleep(1000);
            }, TaskContinuationOptions.LongRunning); //Не уверен в правильности
        }

        private static void PartC()
        {
            Task<bool> task1 = Task.Run(() =>
            {
                Thread.CurrentThread.Name = "My thread";
                Console.WriteLine($"Executing task 1: {Thread.CurrentThread.Name}");
                Thread.Sleep(1000);
                return true;
            });
            Task task2 = task1.ContinueWith(x =>
            {
                Console.WriteLine($"Executing task 2: {Thread.CurrentThread.Name}");
                Thread.Sleep(1000);
            }, TaskContinuationOptions.ExecuteSynchronously);
        }

        private static void PartB()
        {
            Task<bool> task1 = Task.Run(() =>
            {
                Console.WriteLine("Executing task 1");
                Thread.Sleep(1000);
                throw new Exception();
                return true;
            });
            Task task2 = task1.ContinueWith(x =>
            {
                Console.WriteLine("Executing task 2");
                Thread.Sleep(1000);
             }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private static void PartA()
        {
            Task<bool> task1 = Task.Run(() =>
            {
                Console.WriteLine("Executing task 1");
                Thread.Sleep(1000);
                throw new Exception();
                return true;
            });
            Task task2 = task1.ContinueWith(x =>
            {
                Console.WriteLine("Executing task 2");
                Thread.Sleep(1000);
                //}, TaskContinuationOptions.None);
            });
        }
    }
}
