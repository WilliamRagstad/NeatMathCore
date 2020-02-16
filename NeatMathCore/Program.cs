using System;
using ArgumentsUtil;

using NeatMathCore.Math.Classes;

namespace NeatMathCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Arguments a = Arguments.Parse(args);
            Polynomial ex1 = new Polynomial("f", new double[] {5, -9, 2 }, -11);
            Console.WriteLine(ex1);

            Polynomial ex2 = ex1.Derivative() as Polynomial;
            Console.WriteLine(ex2);

            Polynomial ex3 = ex2.Derivative() as Polynomial;
            Console.WriteLine(ex3);

            Polynomial ex4 = ex3.Derivative() as Polynomial;
            Console.WriteLine(ex4);

            Polynomial ex5 = ex4.Derivative() as Polynomial;
            Console.WriteLine(ex5);

            Console.ReadLine();
        }
    }
}
