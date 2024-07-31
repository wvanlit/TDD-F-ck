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

        _sut.Memory.ShouldBeEmpty();
    }

    [Fact]
    public void GivenIncrementValueOperator_IncrementsValueAtMemoryPointer()
    {
        _sut.Interpret("+");

        _sut.Memory.First().Should().Be(1);
        _sut.Memory.Skip(1).ShouldBeEmpty();
    }
    
    [Fact]
    public void GivenMultipleIncrementValueOperators_IncrementsValueAtMemoryPointerMultipleTimes()
    {
        _sut.Interpret("+++++");

        _sut.Memory.First().Should().Be(5);
        _sut.Memory.Skip(1).ShouldBeEmpty();
    }
    
    [Fact]
    public void GivenDecrementValueOperator_DecrementsValueAtMemoryPointer()
    {
        _sut.Interpret("-");

        _sut.Memory.First().Should().Be(255); // 0 - 1 = 255
        _sut.Memory.Skip(1).ShouldBeEmpty();
    }
}