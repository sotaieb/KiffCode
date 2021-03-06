using FluentAssertions;
using NUnit.Framework;
using St.KiffCode.Core.QuickSort;
using System;
using System.Threading.Tasks;

namespace St.KiffCode.Core.Tests.QuickSort
{
    public class NaiveQuickSortHandlerTests
    {
        [TestCase(new int[] { 100, 5, 6, 4, 20 }, new int[] { 4, 5, 6, 20, 100 })]
        [TestCase(new int[] { 50, 70, 40, 100, 30, 60 }, new int[] { 30, 40, 50, 60, 70, 100 })]
        [TestCase(new int[] { 30, 40, 50, 60, 70, 100 }, new int[] { 30, 40, 50, 60, 70, 100 })]
        [TestCase(new int[] { 100, 70, 60, 50, 40, 30 }, new int[] { 30, 40, 50, 60, 70, 100 })]
        public async Task Handle_Test(int[] inputs, int[] expected)
        {
            // Arrange 
            var trace = new RunTrace();
            var request = new QuickSortRequest
            {
                Inputs = inputs,
                Trace = trace
            };
            var sut = new NaiveQuickSortHandler();

            // Act  
            var result = await sut.HandleAsync(request);
            // 1 + 2 +...+ n-2 + n-1 =   4 + 3 + 2 + 1 = 10
            // => ~ n(n-1)/2 ~ (n^2 -n)/2 ~ n^2/2

            // Assert
            Console.WriteLine(String.Join(",", result));
            Console.WriteLine(trace.Counter);
            result.Should().BeEquivalentTo(expected,
                options => options.WithStrictOrdering());
        }
    }
}
