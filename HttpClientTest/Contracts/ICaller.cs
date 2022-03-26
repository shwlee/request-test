using System.Runtime.InteropServices;

namespace HttpClientTest.Contracts;

public interface ICaller
{
    Task<T?> GetAsync<T>(string url, [Optional] object? arguments, [Optional] Dictionary<string, string> headers,  [Optional] string? jwtToken);

    Task<T?> PostAsync<T>(string url, [Optional] object? body, [Optional] Dictionary<string, string> headers,  [Optional] string? jwtToken);
}
