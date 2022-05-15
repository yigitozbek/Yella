namespace Archseptia.Core.Domain.Results
{
    public interface IResult
    {
        bool Success { get; }
        string? Message { get; }
        string ReturnToJson();
    }
}