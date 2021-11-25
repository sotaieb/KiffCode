using St.KiffCode.Core.Abstractions;

namespace St.KiffCode.Core.QuickSort;

public class NaiveQuickSortHandler : IRequestHandler<QuickSortRequest, int[]>
{
    public async Task<int[]> HandleAsync(QuickSortRequest request, CancellationToken token = default)
    {
        if (request is { Inputs.Length: 1 })
        {
            return request.Inputs;
        }

        Sort(request.Inputs, request.Trace);

        return await Task.FromResult(request.Inputs);
    }

    private static void Sort(int[] inputs, RunTrace trace)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            for (int j = i + 1; j < inputs.Length; j++)
            {
                if (inputs[i] > inputs[j])
                {
                    Swap(inputs, i, j);
                }
                trace.Counter++;
            }
        }
    }

    private static void Swap(int[] arr, int i, int j)
    {
        var temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}