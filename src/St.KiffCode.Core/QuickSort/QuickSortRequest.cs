using St.KiffCode.Core.Abstractions;

namespace St.KiffCode.Core.QuickSort
{
    public record QuickSortRequest : IRequest
    {
        public int[] Inputs { get; init; } = default!;
        public RunTrace Trace { get; init; } = default!;
    }
}
