using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace St.KiffCode.Core.Tests.QuickSort;
public class DraftTests
{
    [TestCase(new int[] { 100, 5, 6, 4, 20 })]
    public void Partition_Test(int[] inputs)
    {
        // Arrange            
        var trace = new RunTrace();

        // Act
        var pivotIndex = Partition(inputs, 0, inputs.Length - 1, trace);
        Console.WriteLine(string.Join(",", inputs));

        // Assert

        pivotIndex.Should().Be(3);
        for (int i = 0; i < inputs.Length; i++)
        {
            if (i <= pivotIndex)
            {
                Assert.IsTrue(inputs[i] <= inputs[pivotIndex]);
            }
            else
            {
                Assert.IsTrue(inputs[i] >= inputs[pivotIndex]);
            }
        }
    }
    [TestCase(new int[] { 100, 5, 6, 4, 20 }, new int[] { 4, 5, 6, 20, 100 })]
    [TestCase(new int[] { 50, 70, 40, 100, 30, 60 }, new int[] { 30, 40, 50, 60, 70, 100 })]
    [TestCase(new int[] { 30, 40, 50, 60, 70, 100 }, new int[] { 30, 40, 50, 60, 70, 100 })]
    [TestCase(new int[] { 100, 70, 60, 50, 40, 30 }, new int[] { 30, 40, 50, 60, 70, 100 })]
    public void Iterative_QuickSort_Test(int[] inputs, int[] expected)
    {
        // Arrange
        var trace = new RunTrace();

        // Act
        IterativeQuickSort(inputs, trace);

        // Assert
        Console.WriteLine(string.Join(",", inputs));
        Console.WriteLine(trace.Counter);
        inputs.Should().BeEquivalentTo(expected,
            options => options.WithStrictOrdering());
    }


    public static void IterativeQuickSort(int[] inputs, RunTrace trace) {
       
        var stack = new Stack<(int, int)>();
        stack.Push((0, inputs.Length - 1));

        while (stack.Any())
        {
            var (low, high) = stack.Pop();

            if (low < high)
            {
                int pivotIndex = Partition(inputs, low, high, trace);
                stack.Push((low, pivotIndex - 1));
                stack.Push((pivotIndex + 1, high));
            }
        }
    }

    public static int Partition(int[] arr, int low, int high, RunTrace trace)
    {
        var j = low - 1;
        var pivot = arr[high];

        for (int i = low; i <= high; i++)
        {
            trace.Counter++;
            if (arr[i] <= pivot)
            {
                Swap(arr, i, ++j);
            }
        }
        return j;
    }
    private static void Swap(int[] arr, int i, int j)
    {
        var temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}

