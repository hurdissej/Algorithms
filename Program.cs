using System;
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
            await PrintSomeStuff();
            System.Console.WriteLine("done");
        }

        public static async Task PrintSomeStuff()
        {
            var i = 3;

            Task.Delay(TimeSpan.FromSeconds(5));

            i *= 3;

            Task.Delay(TimeSpan.FromSeconds(5));

            System.Console.WriteLine(i);
        }

    }
}
