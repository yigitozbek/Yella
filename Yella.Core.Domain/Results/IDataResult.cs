namespace Archseptia.Core.Domain.Results
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }

    }
}