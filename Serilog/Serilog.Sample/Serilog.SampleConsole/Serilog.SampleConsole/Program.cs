using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serilog.SampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                //每天会形成一个文件，{Hour}是每小时；{HalfHour}是每半小时;
                .WriteTo.RollingFile("Logs\\log-{Date}.txt")
                .CreateLogger();

            Log.Information("Hello, world!");

            int a = 10, b = 0;
            try
            {
                Log.Debug("Dividing {A} by {B}", a, b);
                Console.WriteLine(a / b);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong");
            }

            Log.CloseAndFlush();
            Console.ReadKey();
        }
    }
}
