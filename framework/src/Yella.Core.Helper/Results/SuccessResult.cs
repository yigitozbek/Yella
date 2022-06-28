namespace Yella.Framework.Helper.Results;

public class SuccessResult : Result, IResult
{
    public SuccessResult(string? message) : base(true, message) { }
    public SuccessResult() : base(true) { }
}