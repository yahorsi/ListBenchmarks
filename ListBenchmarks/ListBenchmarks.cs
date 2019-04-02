using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using System.Collections.Generic;

namespace ListBenchmarks
{
    [DisassemblyDiagnoser(printAsm:true, printIL:true, printSource:true)]
    [Config(typeof(TieredCompilationConfig))]
    public class ListBaseBenchmarks
    {
        private const int LaunchCount = 1;
        private const int WarmupCount = 1000;
        private const int IterationCount = 100;
        private const int InvocationCount = 1000000;


        private class TieredCompilationConfig : ManualConfig
        {
            private const string JitTieredCompilation = "COMPLUS_TieredCompilation";

            public TieredCompilationConfig()
            {
                Add(Job.Core
                    .With(CsProjCoreToolchain.NetCoreApp22)
                    .With(new[] { new EnvironmentVariable(JitTieredCompilation, "0") })
                    .WithId("Tiered Compilation Disabled")
                    .WithLaunchCount(LaunchCount)
                    .WithWarmupCount(WarmupCount)
                    .WithIterationCount(IterationCount)
                    .WithInvocationCount(InvocationCount));

                Add(Job.Core
                    .With(CsProjCoreToolchain.NetCoreApp30)
                    .With(new[] { new EnvironmentVariable(JitTieredCompilation, "0") })
                    .WithId("Tiered Compilation Disabled")
                    .WithLaunchCount(LaunchCount)
                    .WithWarmupCount(WarmupCount)
                    .WithIterationCount(IterationCount)
                    .WithInvocationCount(InvocationCount));

                Add(Job.Core
                    .With(CsProjCoreToolchain.NetCoreApp22)
                    .With(new[] { new EnvironmentVariable(JitTieredCompilation, "1") })
                    .WithId("Tiered Compilation Enabled")
                    .WithLaunchCount(LaunchCount)
                    .WithWarmupCount(WarmupCount)
                    .WithIterationCount(IterationCount)
                    .WithInvocationCount(InvocationCount));

                Add(Job.Core
                    .With(CsProjCoreToolchain.NetCoreApp30)
                    .With(new[] { new EnvironmentVariable(JitTieredCompilation, "1") })
                    .WithId("Tiered Compilation Enabled")
                    .WithLaunchCount(LaunchCount)
                    .WithWarmupCount(WarmupCount)
                    .WithIterationCount(IterationCount)
                    .WithInvocationCount(InvocationCount));
            }
        }

        private List<int> intList;

        [Params(15, 27)]
        public int Index { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            intList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
        }

        [Benchmark]
        public int SumWithAccessByIndex()
        {
            int sum = 0;
            for(int i = 0; i < Index; ++i)
            {
                sum += intList[i];
            }

            return sum;
        }
    }
}
