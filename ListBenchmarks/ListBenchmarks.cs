using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using System.Collections.Generic;

namespace ListBenchmarks
{
    [DisassemblyDiagnoser]
    [Config(typeof(TieredCompilationConfig))]
    [SimpleJob(launchCount: 3, warmupCount: 100, targetCount: 100)]
    public class ListBaseBenchmarks
    {
        private class TieredCompilationConfig : ManualConfig
        {
            private const string JitTieredCompilation = "COMPLUS_TieredCompilation";

            public TieredCompilationConfig()
            {
                Add(Job.Core
                    .With(CsProjCoreToolchain.NetCoreApp22)
                    .With(new[] { new EnvironmentVariable(JitTieredCompilation, "0") })
                    .WithId("Tiered Compilation Disabled"));

                Add(Job.Core
                    .With(CsProjCoreToolchain.NetCoreApp30)
                    .With(new[] { new EnvironmentVariable(JitTieredCompilation, "0") })
                    .WithId("Tiered Compilation Disabled"));

                Add(Job.Core
                    .With(CsProjCoreToolchain.NetCoreApp22)
                    .With(new[] { new EnvironmentVariable(JitTieredCompilation, "1") })
                    .WithId("Tiered Compilation Enabled"));

                Add(Job.Core
                    .With(CsProjCoreToolchain.NetCoreApp30)
                    .With(new[] { new EnvironmentVariable(JitTieredCompilation, "1") })
                    .WithId("Tiered Compilation Enabled"));
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
