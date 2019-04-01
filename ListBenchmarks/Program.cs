using BenchmarkDotNet.Running;

namespace ListBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ListBaseBenchmarks>();
        }
    }
}
