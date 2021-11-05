using System;
using System.Linq;
using System.Threading.Tasks;

namespace MonteCarloPi
{
    public class Program
    {
        private static readonly int cores = Environment.ProcessorCount;
        private static int iterations = 1000000000;

        public static void Main()
        {
            RunSimulation(Environment.GetCommandLineArgs());
        }

        public static int[][] GetThreadIndex(int total)
        {
            var result = new int[cores][];
            var chunk = (total - 1) / cores;
                
            for (int i = 0; i < cores; i++)
            {
                result[i] = new int[2] { i * chunk, (i + 1) * chunk };
            }

            result[cores - 1][1] += ((total - 1) % cores);
            
            return result;
        }

        private static int SInglePIApprox(int min, int max)
        {
            var random = new Random();
            var circleResult = 0;
            double x;

            for (var i = min; i < max; i++)
            {
                x = random.NextDouble();
                var y = random.NextDouble();

                if ((Math.Sqrt(x * x + y * y) <= 1.0))
                {
                    circleResult++;
                }
            }

            return circleResult;
        }

        public static void RunSimulation(string[] arguments)
        {
            if (arguments?.Length > 1)
            {
                if (int.TryParse(arguments[1], out int input))
                {
                    iterations = input;
                }
                else
                {
                    Console.WriteLine("String could not be parsed.");
                    Environment.Exit(0);
                }
            }

            var sum = new int[cores];
            var range = GetThreadIndex(iterations);
            
            Parallel.For(0, cores, i =>
            {
                sum[i] = SInglePIApprox(range[i][0], range[i][1]);
            });

            var inCircle = sum.Sum();

            var piApprox = 4 * (inCircle / (double)iterations);

            Console.WriteLine($"Approx Pi:{piApprox:F21}");
            Console.WriteLine($"Actual Pi:{Math.PI:F21}");
        }
    }
}
