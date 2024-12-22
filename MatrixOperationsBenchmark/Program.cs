using System;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

// Run the benchmarks using BenchmarkDotNet
topLevelStatements();

void topLevelStatements()
{
    BenchmarkRunner.Run<MatrixOperationsBenchmarks>();
}

[MemoryDiagnoser]
public class MatrixOperationsBenchmarks
{
    private int[,] matrixA;
    private int[,] matrixB;

    [GlobalSetup]
    public void Setup()
    {
        // Matrix size: 500x500
        const int size = 500; 

        matrixA = GenerateMatrix(size, size);
        matrixB = GenerateMatrix(size, size);
    }
     
    [Benchmark]
    public int[,] MultiplyUsingForLoops()
    {
        int rows = matrixA.GetLength(0);
        int cols = matrixB.GetLength(1);
        int[,] result = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                for (int k = 0; k < matrixA.GetLength(1); k++)
                {
                    result[i, j] += matrixA[i, k] * matrixB[k, j];
                }
            }
        }

        return result;
    }
     
    [Benchmark]
    public int[,] MultiplyUsingParallelFor()
    {
        int rows = matrixA.GetLength(0);
        int cols = matrixB.GetLength(1);
        int[,] result = new int[rows, cols];

        Parallel.For(0, rows, i =>
        {
            for (int j = 0; j < cols; j++)
            {
                for (int k = 0; k < matrixA.GetLength(1); k++)
                {
                    result[i, j] += matrixA[i, k] * matrixB[k, j];
                }
            }
        });

        return result;
    }

    
    [Benchmark]
    public int[,] MultiplyUsingPlinq()
    {
        int rows = matrixA.GetLength(0);
        int cols = matrixB.GetLength(1);

        var result = Enumerable.Range(0, rows).AsParallel().Select(row =>
        {
            int[] rowResult = new int[cols];

            for (int j = 0; j < cols; j++)
            {
                for (int k = 0; k < matrixA.GetLength(1); k++)
                {
                    rowResult[j] += matrixA[row, k] * matrixB[k, j];
                }
            }

            return rowResult;
        }).ToArray();

        return ConvertJaggedTo2D(result);
    }

    // we generate a random matrix with values between 1 and 10 for simplicity
    private static int[,] GenerateMatrix(int rows, int cols)
    {
        var random = new Random();
        var matrix = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = random.Next(1, 10);
            }
        }

        return matrix;
    }

    private static int[,] ConvertJaggedTo2D(int[][] jaggedArray)
    {
        int rows = jaggedArray.Length;
        int cols = jaggedArray[0].Length;
        int[,] result = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = jaggedArray[i][j];
            }
        }

        return result;
    }
}
