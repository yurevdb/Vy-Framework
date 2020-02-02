using System;
using Vy;

namespace Vy.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {

            TaskEngine.Run("HW", () =>
            {
                Console.WriteLine("Sup");
            });
            
            TaskEngine.Run("HW", () =>
            {
                Console.WriteLine("Yo");
            });

            var x = TaskEngine.GetTask("HW");

            TaskEngine.WaitAll();

            _ = 5;
        }
    }
}
