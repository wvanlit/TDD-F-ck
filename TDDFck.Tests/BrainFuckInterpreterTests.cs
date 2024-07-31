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
    
    [Fact]
    public void GivenMultipleDecrementValueOperators_DecrementsValueAtMemoryPointerMultipleTimes()
    {
        _sut.Interpret("-----");

        _sut.Memory.First().Should().Be(251); // 0 - 5 = 251
        _sut.Memory.Skip(1).ShouldBeEmpty();
    }
    
    [Fact]
    public void GivenMultipleValueOperators_SetValueAtMemoryPointerCorrectly()
    {
        _sut.Interpret("+++++-----+");

        _sut.Memory.First().Should().Be(1); // 5 - 5 + 1 = 1
        _sut.Memory.Skip(1).ShouldBeEmpty();
    }
    
    [Fact]
    public void GivenIncreaseMemoryPointerOperator_MovesMemoryPointerCorrectly()
    {
        _sut.Interpret("+>+>+");

        _sut.Memory.Take(3).ShouldBe(1, 1, 1);
        _sut.Memory.Skip(3).ShouldBeEmpty();
    }
    
    [Fact]
    public void GivenDecreaseMemoryPointerOperator_MovesMemoryPointerCorrectly()
    {
        _sut.Interpret(">>>+<<++");
        
        _sut.Memory.Take(4).ShouldBe(0, 2, 0, 1);
        _sut.Memory.Skip(4).ShouldBeEmpty();
    }
}