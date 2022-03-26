using System;

namespace ServiceTest;

public class ResponseModel
{
    public string? WorkerName { get; set; }
    
    public int Count { get; set; }
    
    public object? Model { get; set; }
}

public class TestContext
{
    public string? Name { get; set; }

    public int Id { get; set; }
}

public class PostRequestModel
{
    public enum PostType
    {
        Direct,

        Indoor
    }

    public string? Name { get; set; }

    public DateTime CreatedAt {get;set;}

    public PostType Type { get; set; }

    public TestContext Context { get; set; }
}