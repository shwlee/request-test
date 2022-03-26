using System;
using System.Collections;
using System.Collections.Generic;

namespace ServiceTest.Mocks;

internal class CallerHeaderTestMockups : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new Dictionary<string, string> { { "open", "gate" }, { "function", "metadata" }, { "role", "admin" } } };
        yield return new object[] { new Dictionary<string, string> { { "xkey", "keeper" }, { "internal", "func" }, { "role", "viewer" } } };
        yield return new object[] { new Dictionary<string, string> { { "role", "opener" }, { "min", "greater0" }, { "max", "less10" } } };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

internal class CallerParamsTestMockups : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new { seq = 101, from = "admin", } };
        yield return new object[] { new { itemId = "qwe123asd", type = 0, to = "https://naver.com" } };
        yield return new object[] { new { no = 10, blogNo = "naver.1", isPrivate = false, ratio = 100200 } };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

internal class CallerRequestObjectTestMockups : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new PostRequestModel
            {
                Type= PostRequestModel.PostType.Indoor,
                Name = "test-poor",
                CreatedAt = DateTime.UtcNow,
                Context = new TestContext
                {
                    Id = 2,
                    Name = "next context"
                }
            }
        };
        yield return new object[] { new PostRequestModel
            {
                Type= PostRequestModel.PostType.Indoor,
                Name = "test-rich",
                CreatedAt = DateTime.UtcNow.AddMinutes(10),
                Context = new TestContext
                {
                    Id = 5,
                    Name = "5 to"
                }
            }
        };
        yield return new object[] { new PostRequestModel
            {
                Type= PostRequestModel.PostType.Direct,
                Name = "test-gold",
                CreatedAt = DateTime.UtcNow.AddMinutes(12),
                Context = new TestContext
                {
                    Id = 101,
                    Name = "101 context"
                }
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
