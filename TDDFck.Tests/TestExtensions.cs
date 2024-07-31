namespace TDDFck.Tests;

public static class TestExtensions
{
    public static void ShouldBeEmpty(this IEnumerable<byte> memory)
    {
        memory.Should().AllSatisfy(m => m.Should().Be(0));
    }
    
    public static void ShouldBe(this IEnumerable<byte> memory, params byte[] values)
    {
        memory.Should().BeEquivalentTo(values);
    }
    
    public static void ShouldBe(this IEnumerable<byte> memory, string s)
    {
        memory.Should().BeEquivalentTo(s.Select(v => (byte)v));
    }
}