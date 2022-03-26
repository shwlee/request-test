using System.Collections.Generic;

namespace ServiceTest;

public class TestEcho
{
    public string? Url { get; set; }

    public string? Method { get; set; }

    public Dictionary<string, string>? Headers { get; set; }

    public string? Token { get; set; }

    public string? Query { get; set; }
}
