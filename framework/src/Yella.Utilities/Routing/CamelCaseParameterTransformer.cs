using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace Yella.Utilities.Routing;

/// <summary>
/// This method separates API names with hyphens. For example it translates 'api/get-address' to 'api/GetAddress'
/// 
/// builder.Services.AddControllers(options =>
/// {
///     options.Conventions.Add(new RouteTokenTransformerConvention(new CamelCaseParameterTransformer()));
///     options.EnableEndpointRouting = false;
/// });
/// 
/// </summary>
public class CamelCaseParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value == null ? null : Regex.Replace(value.ToString() ?? string.Empty, "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}