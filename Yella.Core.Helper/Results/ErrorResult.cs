namespace Yella.Core.Helper.Results;

public class ErrorResult : Result, IResult
{
    public ErrorResult(string? message, int? statusCode) : base(false, message, statusCode)
    {

    }
    public ErrorResult(string? message) : base(false, message)
    {

    }

    public ErrorResult(int? statusCode) : base(false, statusCode)
    {

    }
}