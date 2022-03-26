using System.Web;

namespace HttpClientTest.Extensions;

public static class ParamerterExtesion
{
    public static string ToQueryString(this object obj)
    {
        var properties = from p in obj.GetType().GetProperties()
                         where p.GetValue(obj, null) != null
                         select $"{p.Name}={HttpUtility.UrlEncode(p.GetValue(obj, null)?.ToString())}";

        return string.Join("&", properties.ToArray());
    }
}
