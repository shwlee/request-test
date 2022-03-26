using FluentAssertions;
using HttpClientTest.Contracts;
using HttpClientTest.Extensions;
using HttpClientTest.Services;
using ServiceTest.Mocks;
using ServiceTest.MockServer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ServiceTest;
public class Test : IClassFixture<TestServer>
{
    [Fact]
    public void Caller_implementation_is_valid()
    {
        var caller = GetCaller();
        caller.Should().NotBeNull();
    }

    [Fact]
    public async Task Url_can_request_to_get()
    {
        var caller = GetCaller();
        var url = "http://localhost:19001/test/echo";
        var response = await caller.GetAsync<TestEcho>(url);
        response.Should().NotBeNull();
        response.Url.Should().BeEquivalentTo(url);
    }

    [Theory]
    [ClassData(typeof(CallerParamsTestMockups))]
    public async Task Parameters_are_generated_for_valid_http_query_strings(object? param)
    {
        var caller = GetCaller();
        var url = "http://localhost:19001/test/echo";
        var response = await caller.GetAsync<TestEcho>(url, param);
        response.Should().NotBeNull();

        var queryStrings = param.ToQueryString(); // ���⼭�� ToQueryString() ������ �׽�Ʈ�� �Ϸ�Ǿ��ٰ� ����.
        response.Query.Should().BeEquivalentTo(queryStrings);
    }

    [Theory]
    [ClassData(typeof(CallerHeaderTestMockups))]
    public async Task Custom_headers_are_set_for_valid_http_header_by_post(Dictionary<string, string> headers)
    {
        var url = "http://localhost:19001/test/echo";
        var caller = GetCaller();
        var response = await caller.PostAsync<TestEcho>(url, null, headers);
        response.Should().NotBeNull();

        // ��°�� ���ϸ� �⺻ header���� ���ԵǹǷ� ��û�� custom header ���θ� ���Ѵ�.
        var allHaveHeaders = headers.All(header => response.Headers[header.Key].ToString() == header.Value);
        allHaveHeaders.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(CallerHeaderTestMockups))]
    public async Task Custom_headers_are_set_for_valid_http_header_by_get(Dictionary<string, string> headers)
    {
        var url = "http://localhost:19001/test/echo";
        var caller = GetCaller();
        var response = await caller.GetAsync<TestEcho>(url, null, headers);
        response.Should().NotBeNull();

        // ��°�� ���ϸ� �⺻ header���� ���ԵǹǷ� ��û�� custom header ���θ� ���Ѵ�.
        var allHaveHeaders = headers.All(header => response.Headers[header.Key].ToString() == header.Value);
        allHaveHeaders.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(CallerRequestObjectTestMockups))]
    public async Task Request_object_is_correct_to_response(PostRequestModel model)
    {
        var url = "http://localhost:19001/test/model";
        var caller = GetCaller();

        var response = await caller.PostAsync<PostRequestModel>(url, model);
        response.Should().NotBeNull();
        response.Should().BeEquivalentTo(model);
    }

    private ICaller GetCaller()
    {
        return new HttpCaller();
    }
}