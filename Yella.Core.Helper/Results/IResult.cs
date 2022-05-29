namespace Yella.Core.Helper.Results;

public interface IResult
{
    bool Success { get; }
    string? Message { get; }
}