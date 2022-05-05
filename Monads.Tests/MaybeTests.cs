using Xunit;

namespace Monads.Tests;

public class MaybeTests
{
    [Fact]
    public void Test1() => Maybe<int>.Some(3).Map(value => value + "");
}