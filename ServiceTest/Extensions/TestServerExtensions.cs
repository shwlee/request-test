using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace ServiceTest.Extensions;

public static class TestServerExtensions
{
    public static IEndpointRouteBuilder MapGets(this IEndpointRouteBuilder app)
    {
        app.MapGet("/test/model", (string id) =>
        {
            if (string.CompareOrdinal(id, "test") is not 0)
            {
                throw new BadHttpRequestException($"It is not match id parameter. id:{id}");
            }

            var context = new ResponseModel
            {
                WorkerName = "test response",
                Count = 10,
                Model = new TestContext
                {
                    Name = "incoming@@#$%",
                    Id = (int)DateTime.UtcNow.Ticks
                }
            };

            return context;
        });

        app.MapGet("/test/echo", (HttpContext context) =>
        {
            var echo = new TestEcho
            {
                Method = context.Request.Method,
                Token = context.Request.Headers.Authorization.ToString(),
                Url = $"{context.Request.Scheme}://{context.Request.Host.Value}{context.Request.Path}",
                Headers = context.Request.Headers.ToDictionary(header => header.Key, header => header.Value.ToString()),
                Query = string.Join("&", context.Request.Query.Select(q => $"{q.Key}={HttpUtility.UrlEncode(q.Value.ToString())}"))
            };

            return echo;
        });

        return app;
    }

    public static IEndpointRouteBuilder MapPosts(this IEndpointRouteBuilder app)
    {
        app.MapPost("/test/model", (PostRequestModel model) =>
        {
            return model;
        });

        app.MapPost("/test/echo", (HttpContext context) =>
        {
            var echo = new TestEcho
            {
                Method = context.Request.Method,
                Token = context.Request.Headers.Authorization.ToString(),
                Url = context.Request.Host.Value,
                Headers = context.Request.Headers.ToDictionary(header => header.Key, header => header.Value.ToString()),
            };

            return echo;
        });

        return app;
    }
}
