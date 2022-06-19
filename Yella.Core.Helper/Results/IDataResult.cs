namespace Yella.Framework.Helper.Results;

public interface IDataResult<out T> : IResult
{
    T Data { get; }

}