using Newtonsoft.Json;

namespace Yella.Framework.CrossCuttingConcern.Validations.FluentValidation;

public class ValidationError
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? Field { get; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Code { get; set; }

    public string Message { get; }

    public ValidationError(string? field, string code, string message)
    {
        Field = field != string.Empty ? field : null;
        Message = message;
    }
}