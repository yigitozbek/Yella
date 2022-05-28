namespace Yella.Core.Helper.Results;

public class DataResult<T> : Result, IDataResult<T>
{
    public DataResult(T data, bool success, string? message, int? statusCode) : base(success, message, statusCode)
    {
        Data = data;
    }

    public DataResult(T data, bool success, string? message) : base(success, message)
    {
        Data = data;
    }


    public DataResult(T data, bool success, int? statusCode) : base(success, statusCode)
    {
        Data = data;
    }

    public T Data { get; }
}