namespace Yella.Core.Helper.Results;

public interface IDataResult<out T> : IResult
{
    T Data { get; }

}