using System;
using System.Threading;

namespace DotNetRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using(var robot = new Robot())
            {
                robot.Forwards();
                Thread.Sleep(1000);

                robot.Backwards();
                Thread.Sleep(1000);
                
                robot.Right();
                Thread.Sleep(1000);
                
                robot.Left();
                Thread.Sleep(1000);
                
                robot.Stop();
            }
        }
    }
}
