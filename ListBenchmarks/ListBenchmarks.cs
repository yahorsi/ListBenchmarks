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
