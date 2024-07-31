namespace TDDFck.Tests;

public class BrainFuckInterpreterTests
{
    private readonly BrainFuckInterpreter _sut;
    private readonly TestCharacterInput _testInput = new ();
    private readonly TestCharacterOutput _testOutput = new ();

    public BrainFuckInterpreterTests()
    {
        _sut = new BrainFuckInterpreter(1024, _testInput, _testOutput);
    }
    
    [Fact]
    public void GivenEmptyInput_DoesNotThrow()
    {
        _sut.Interpret("");
    }
    
    [Fact]
    public void GivenEmptyInput_MemoryIsZero()
    {
        _sut.Interpret("");

        _sut.Memory.Should().AllSatisfy(m => m.Should().Be(0u));
    }

    [Fact]
    public void GivenIncrementValueOperator_IncrementsValueAtMemoryPointer()
    {
        _sut.Interpret("+");

        _sut.Memory.First().Should().Be(1u);
        _sut.Memory.Skip(1).Should().AllSatisfy(m => m.Should().Be(0u));
    }
}