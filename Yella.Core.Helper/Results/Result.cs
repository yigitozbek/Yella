namespace Yella.Core.Helper.Results;

public class Result : IResult
{
    public Result(bool success,  string? message, int? statusCode) : this(success, statusCode)
    {
        Message = message;
    }

    public Result(bool success, int? statusCode)
    {
        Success = success;
        StatusCode = statusCode;
    }
    public Result(bool success, string? message)
    {
        Success = success;
        Message = message;
    }

    public int? StatusCode { get; set; }

    public bool Success { get; }

    public string? Message { get; }
}