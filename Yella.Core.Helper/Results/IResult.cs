namespace Yella.Framework.Helper.Results;

public interface IResult
{
    bool Success { get; }
    string? Message { get; }
}