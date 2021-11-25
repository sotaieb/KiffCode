using St.KiffCode.Core.Abstractions;

namespace St.KiffCode.Core.QuickSort;

public class IterativeQuickSortHandler : IRequestHandler<QuickSortRequest, int[]>
{
    public async Task<int[]> HandleAsync(QuickSortRequest request, CancellationToken token = default)
    {
        if (request is { Inputs.Length: 1 })
        {
            return await Task.FromResult(request.Inputs);
        }
             
        int pivotIndex;

        var stack = new Stack<(int, int)>();
        stack.Push((0, request.Inputs.Length - 1));

        while (stack.Any())
        {
            var (low, high) = stack.Pop();

            if (low < high)
            {
                Console.WriteLine($"{low}-{high}");
                pivotIndex = Partition(request.Inputs, low, high, request.Trace);
                stack.Push((low, pivotIndex - 1));
                stack.Push((pivotIndex + 1, high));
            }

        }
        return await Task.FromResult(request.Inputs);
    }

    private static int Partition(int[] arr, int low, int high, RunTrace trace)
    {
        var pivot = arr[high];

        var j = low - 1;
        for (int i = low; i <= high; i++)
        {
            if (arr[i] <= pivot)
            {
                Swap(arr, i, ++j);
            }
            trace.Counter++;
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