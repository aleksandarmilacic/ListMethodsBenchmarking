using System;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Columns;

// Run the benchmarks using BenchmarkDotNet
topLevelStatements();

void topLevelStatements()
{
    var summary = BenchmarkRunner.Run<ListModificationBenchmarks>(DefaultConfig.Instance
        .WithSummaryStyle(SummaryStyle.Default.WithMaxParameterColumnWidth(50)));
}


[MemoryDiagnoser]
public class ListModificationBenchmarks
{
    private int[] data;

    [GlobalSetup]
    public void Setup()
    {
        const int ListSize = 100_000;
        data = Enumerable.Range(1, ListSize).ToArray();
    }

    [Benchmark]
    public void ForLoop()
    {
        var array = (int[])data.Clone();
        for (int i = 0; i < array.Length; i++)
        {
            array[i] *= 2;
        }
    }

    [Benchmark]
    public void ForEachLoop()
    {
        var array = (int[])data.Clone();
        int index = 0;
        foreach (var item in array)
        {
            array[index++] = item * 2;
        }
    }

    [Benchmark]
    public void LinqSelect()
    {
        var array = (int[])data.Clone();
        var result = array.Select(x => x * 2).ToArray();
    }

    [Benchmark]
    public void ParallelFor()
    {
        var array = (int[])data.Clone();
        Parallel.For(0, array.Length, i =>
        {
            array[i] *= 2;
        });
    }

    [Benchmark]
    public void Span()
    {
        var array = (int[])data.Clone();
        Span<int> span = array;
        for (int i = 0; i < span.Length; i++)
        {
            span[i] *= 2;
        }
    }

    [Benchmark]
    public void Plinq()
    {
        var array = (int[])data.Clone();
        var result = array.AsParallel().Select(x => x * 2).ToArray();
    }
}
