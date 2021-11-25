using St.KiffCode.Core.Abstractions;

namespace St.KiffCode.Core.QuickSort;

public class RecursiveQuickSortHandler : IRequestHandler<QuickSortRequest, int[]>
{
    public async Task<int[]> HandleAsync(QuickSortRequest request, CancellationToken token=default)
    {
        if (request is { Inputs.Length: 1 })
        {
            return await Task.FromResult(request.Inputs);
        }

        QuickSort(request.Inputs, 0, request.Inputs.Length - 1, request.Trace);

        return await Task.FromResult(request.Inputs);
    }

     private static void QuickSort(int[] arr, int low, int high, RunTrace trace) {

         if (low >= 0 && high >= 0 && low < high) {
             var p = Partition(arr, low, high, trace);
             QuickSort(arr, low, p -1, trace);
             QuickSort(arr, p + 1, high, trace);
         }            
     }

     private static int Partition(int[] arr, int low, int high, RunTrace trace) {
         var pivot = arr[high];

         var j = low - 1;
         for (int i = low; i <= high; i++)
         {
             if (arr[i] <= pivot) {                  
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