using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome1574();
            Welcome3963();
            Console.ReadKey();
        }

        static partial void Welcome3963();
        private static void Welcome1574()
        {
            Console.Write("Enter your name: ");
            string name = System.Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
