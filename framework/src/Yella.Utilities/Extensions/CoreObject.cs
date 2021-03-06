using Newtonsoft.Json;

namespace Yella.Utilities.Extensions;

public static class CoreObject
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

    /// <summary>
    /// This method is used to convert to object to Datetime
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string obj)
    {
        var result = Convert.ToDateTime(obj);
        return result;
    }



}