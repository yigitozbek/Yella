using Newtonsoft.Json;

namespace Yella.Core.Helper.Extensions;

public static class Object
{
    public static string ToJson(this object obj)
    {
        var result = JsonConvert.SerializeObject(obj);
        return result;
    }


}