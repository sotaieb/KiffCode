namespace St.KiffCode.Core.Abstractions;
public interface IRequestHandler<in REQUEST, RESPONSE> where REQUEST : IRequest
{
    Task<RESPONSE> HandleAsync(REQUEST request, CancellationToken token = default);
}