using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace ListBenchmarks
{
    public class ListBaseBenchmarks
    {
        private List<int> intList;

        [Params(1, 5, 9)]
        public int Index { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            intList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }

        [Benchmark]
        public int AccessByIndex()
        {
            return intList[Index];
        }
    }
}
