using Microsoft.AspNetCore.Builder;
using ServiceTest.Extensions;
using System;
using System.Diagnostics;

namespace ServiceTest.MockServer;

public class TestServer : IDisposable
{
    private WebApplication? _app;

    public TestServer()
    {
        CreateAndStart();
    }

    private void CreateAndStart()
    {
        var uri = "http://0.0.0.0:19001";
        var builder = WebApplication.CreateBuilder();

        _app = builder.Build();
        _app.Urls.Add(uri);
        _app.MapGets().MapPosts();

        _app.RunAsync();
    }

    public void Dispose()
    {
        Debug.WriteLine("[shwlee] ~~~~~~~~~~~~~~~~~~~~~~~");
        (_app as IDisposable)?.Dispose();
    }
}
