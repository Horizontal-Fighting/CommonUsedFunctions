using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Async_Await
{
    class Program
    {
        static void Main(string[] args)
        {
            Method1();
            Method2();
            Console.WriteLine("Main Thread");
            Console.ReadLine();
        }

        public static void Method1()
        {
            Task.Run(new Action(LongTask));
            Console.WriteLine("Normal New Thread");
        }

        public static async void Method2()
        {
            await Task.Run(new Action(LongTask));
            Console.WriteLine("New Async Thread");//wait until the long task finishes
        }

        public static void LongTask()
        {
            System.Threading.Thread.Sleep(5000);
        }
    }
}
