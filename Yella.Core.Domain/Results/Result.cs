using Newtonsoft.Json;

namespace Yella.Core.Domain.Results
{
    public class Result : IResult
    {
        public Result(bool success, string? message) : this(success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public string ReturnToJson()
        {
            return JsonConvert.SerializeObject(new { success = Success, message = Message });
        }

        public bool Success { get; }
        public string? Message { get; }
    }
}