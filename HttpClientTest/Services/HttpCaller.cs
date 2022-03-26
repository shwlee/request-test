using HttpClientTest.Contracts;
using HttpClientTest.Extensions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace HttpClientTest.Services;

public class HttpCaller : ICaller
{
    private HttpClient _httpClient = new();

    public async Task<T?> GetAsync<T>(string url, [Optional] object? arguments, [Optional] Dictionary<string, string> headers, [Optional] string? jwtToken)
    {
        if (arguments is not null)
        {
            url = $"{url}?{arguments.ToQueryString()}";
        }

        // send request        
        using var response = await GetResponse(url, HttpMethod.Get, null, headers, jwtToken);
        var result = await response.Content.ReadFromJsonAsync<T>();
        return result;
    }

    public async Task<T?> PostAsync<T>(string url, [Optional] object? body, [Optional] Dictionary<string, string> headers, [Optional] string? jwtToken)
    {
        using var response = await GetResponse(url, HttpMethod.Post, body, headers, jwtToken);
        var result = await response.Content.ReadFromJsonAsync<T>();
        return result;
    }

    private async Task<HttpResponseMessage> GetResponse(
        string url,
        HttpMethod method,
        [Optional] object? body,
        [Optional] Dictionary<string, string> headers,
        [Optional] string? jwtToken)
    {
        var request = new HttpRequestMessage(method, url);
        if (body is not null)
        {
            request.Content = JsonContent.Create(body);
        }

        PrepareHttpClient(request, headers, jwtToken);

        // send request
        return await _httpClient.SendAsync(request);
    }

    private void PrepareHttpClient(HttpRequestMessage request, [Optional] Dictionary<string, string>? headers, [Optional] string? jwtToken)
    {
        if (headers is not null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        if (string.IsNullOrWhiteSpace(jwtToken) is false)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }
    }
}
