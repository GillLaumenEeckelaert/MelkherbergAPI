using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Framework.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public class BaseCodeType<T> where T : Enum
{
    public static IEnumerable<T> GetAllValues()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }
}

public static class AttributeHelperExtension
{
    public static string ToDescription(this Enum value)
    {
        var da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString()))!.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return da.Length > 0 ? da[0].Description : value.ToString();
    }
}