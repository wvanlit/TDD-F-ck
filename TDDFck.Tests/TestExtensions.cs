namespace TDDFck.Tests;

public static class TestExtensions
{
    public static void ShouldBeEmpty(this IEnumerable<byte> memory)
    {
        memory.Should().AllSatisfy(m => m.Should().Be(0));
    }
}