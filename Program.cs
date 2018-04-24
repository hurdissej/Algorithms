using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Algorithms.Graphs;

namespace Algorithms
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var s = new Stopwatch();
            s.Start();
            await PrintSomeStuff();
            System.Console.WriteLine(s.ElapsedMilliseconds);
            System.Console.WriteLine("done");
        }

        public static async Task PrintSomeStuff()
        {
            var i = 3;

            await Task.Delay(TimeSpan.FromSeconds(5));

            i *= 3;

            await Task.Delay(TimeSpan.FromSeconds(5));

            System.Console.WriteLine(i);
        }

    }
}
