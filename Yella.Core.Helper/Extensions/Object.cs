using Newtonsoft.Json;

namespace Yella.Core.Helper.Extensions;

public static class Object
{
    /// <summary>
    /// This method is used to convert the object to JSON.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ToJson(this object obj)
    {
        var result = JsonConvert.SerializeObject(obj);
        return result;
    }


}