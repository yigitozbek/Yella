namespace Yella.Utilities.Results;

public class ErrorResult : Result, IResult
{
    public ErrorResult(string? message) : base(false, message)
    {

    }
}