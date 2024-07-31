namespace TDDFck.Tests;

public class BrainFuckInterpreterTests
{
    private const uint MemorySize = 256;
    private readonly BrainFuckInterpreter _sut;
    private readonly TestCharacterInput _testInput = new ();
    private readonly TestCharacterOutput _testOutput = new ();

    public BrainFuckInterpreterTests()
    {
        _sut = new BrainFuckInterpreter(MemorySize, _testInput, _testOutput);
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

    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenIncrementValueOperator_IncrementsValueAtMemoryPointer()
    {
        _sut.Interpret("+");

        _sut.Memory.First().Should().Be(1);
        _sut.Memory.Skip(1).ShouldBeEmpty();
    }
    
    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenMultipleIncrementValueOperators_IncrementsValueAtMemoryPointerMultipleTimes()
    {
        _sut.Interpret("+++++");

        _sut.Memory.First().Should().Be(5);
        _sut.Memory.Skip(1).ShouldBeEmpty();
    }
    
    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenDecrementValueOperator_DecrementsValueAtMemoryPointer()
    {
        _sut.Interpret("-");

        _sut.Memory.First().Should().Be(255); // 0 - 1 = 255
        _sut.Memory.Skip(1).ShouldBeEmpty();
    }
    
    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenMultipleDecrementValueOperators_DecrementsValueAtMemoryPointerMultipleTimes()
    {
        _sut.Interpret("-----");

        _sut.Memory.First().Should().Be(251); // 0 - 5 = 251
        _sut.Memory.Skip(1).ShouldBeEmpty();
    }
    
    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenMultipleValueOperators_SetValueAtMemoryPointerCorrectly()
    {
        _sut.Interpret("+++++-----+");

        _sut.Memory.First().Should().Be(1); // 5 - 5 + 1 = 1
        _sut.Memory.Skip(1).ShouldBeEmpty();
    }
    
    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenIncreaseMemoryPointerOperator_MovesMemoryPointerCorrectly()
    {
        _sut.Interpret("+>+>+");

        _sut.Memory.Take(3).ShouldBe(1, 1, 1);
        _sut.Memory.Skip(3).ShouldBeEmpty();
    }
    
    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenDecreaseMemoryPointerOperator_MovesMemoryPointerCorrectly()
    {
        _sut.Interpret(">>>+<<++");
        
        _sut.Memory.Take(4).ShouldBe(0, 2, 0, 1);
        _sut.Memory.Skip(4).ShouldBeEmpty();
    }

    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenMemoryPointerDecreasesBelowZero_WrapsAroundToLastCell()
    {
        _sut.Interpret("<+");

        _sut.Memory.Last().Should().Be(1);
        _sut.Memory.SkipLast(1).ShouldBeEmpty();
    }
    
    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenMemoryPointerIncreasesPastLastCell_WrapsAroundToFirstCell()
    {
        _sut.Interpret(new string('>', (int)MemorySize) + "+");

        _sut.Memory.First().Should().Be(1);
        _sut.Memory.Skip(1).ShouldBeEmpty();
    }

    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenInputOperator_ReadsFromInputAndWritesAsciiValueToCurrentCell()
    {
        _testInput.SetInput("a");
        
        _sut.Interpret(",");
        
        _sut.Memory.First().Should().Be((byte)'a');
        _sut.Memory.Skip(1).ShouldBeEmpty();
    }
    
    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenMultipleInputOperators_ReadsFromInputAndWritesAsciiValuesToMemory()
    {
        _testInput.SetInput("abc");
        
        _sut.Interpret(",>,>,");
        
        _sut.Memory.Take(3).ShouldBe("abc");
        _sut.Memory.Skip(3).ShouldBeEmpty();
    }
    
    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenOutputOperator_ReadsFromCellAndWritesAsciiValueToOutput()
    {
        _testInput.SetInput("a");
        
        _sut.Interpret(",.");
        
        _testOutput.Output.Should().Be("a");
    }
    
    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenMultipleOutputOperators_ReadsFromCellAndWritesAllAsciiValuesToOutput()
    {
        _testInput.SetInput("abcABC");
        
        _sut.Interpret(",>,>,>,>,>,><<<<<<.>.>.>.>.>.");
        
        _testOutput.Output.Should().Be("abcABC");
    }
    
    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenJumpOperators_LoopsUntilCellIsZero()
    {
        _sut.Interpret("++++++++++[--]");
        
        _sut.Memory.ShouldBeEmpty();
    }
    
    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenMultipleJumpOperators_LoopsUntilBothCellsAreZeroAgain()
    {
        _sut.Interpret("+++++[-]>+++++[-]");
        
        _sut.Memory.ShouldBeEmpty();
    }
    
    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenNestedJumpOperators_LoopsUntilAllCellsAreZeroAgain()
    {
        _testInput.SetInput("abcd");
        
        _sut.Interpret(",>,>,>,[.[-]<]");
        
        _sut.Memory.ShouldBeEmpty();
        _testOutput.Output.Should().Be("dcba");
    }

    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenJumpOperatorHitWhenCellAlreadyAtZero_SkipsRunningTheLoop()
    {
        _sut.Interpret("+++++>[+++>+++>+++]+");

        _sut.Memory.ElementAt(0).Should().Be(5);
        _sut.Memory.ElementAt(1).Should().Be(1);
        _sut.Memory.Skip(2).ShouldBeEmpty();
    }
    
    [Fact(Skip = "Unskip this when all tests above pass")]
    public void GivenNonOperatorCharacter_IgnoresCharacter()
    {
        _sut.Interpret(@" !#$%&'()*/0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ\^_`abcdefghijklmnopqrstuvwxyz{|}~ ¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ");
        
        _sut.Memory.ShouldBeEmpty();
    }
    
    /*
     * Congratulations! You're interpreter should now be 100% functional.
     *
     * Wanna find out? The tests below are some more complex brainfuck programs
     */

    [Fact(Skip = "Unskip this when you are done building the interpreter")]
    public void GivenHelloWorld_PrintsHelloWorld()
    {
        _sut.Interpret("+++++++++++[>++++++>+++++++++>++++++++>++++>+++>+<<<<<<-]>++++++.>++.+++++++..+++.>>.>-.<<-.<.+++.------.--------.>>>+.");

        _testOutput.Output.Should().BeEquivalentTo("Hello, World!");
    }

    [Fact(Skip = "Unskip this when you are done building the interpreter")]
    public void GivenShortHelloWorld_PrintsHelloWorld()
    {
        _sut.Interpret("+[-->-[>>+>-----<<]<--<---]>-.>>>+.>>..+++[.>]<<<<.+++.------.<<-.>>>>+.");

        _testOutput.Output.Should().BeEquivalentTo("Hello, World!");
    }

    [Fact(Skip = "Unskip this when you are done building the interpreter")]
    public void GivenCellSizeCalculator_Returns8BitCells()
    {
        var code = @"
        Calculate the value 256 and test if it's zero
        If the interpreter errors on overflow this is where it'll happen
        ++++++++[>++++++++<-]>[<++++>-]
        +<[>-<
            Not zero so multiply by 256 again to get 65536
            [>++++<-]>[<++++++++>-]<[>++++++++<-]
            +>[>
                # Print ""32""
                ++++++++++[>+++++<-]>+.-.[-]<
            <[-]<->] <[>>
                # Print ""16""
                +++++++[>+++++++<-]>.+++++.[-]<
        <<-]] >[>
            # Print ""8""
            ++++++++[>+++++++<-]>.[-]<
        <-]<
        # Print "" bit cells\n""
        +++++++++++[>+++>+++++++++>+++++++++>+<<<<-]>-.>-.+++++++.+++++++++++.<.
        >>.++.+++++++..<-.>>-
        Clean up used cells.
        [[-]<]
        ";
        
        _sut.Interpret(code);
        
        _testOutput.Output.Should().BeEquivalentTo("8 bit cells\n");
    }

    [Fact(Skip = "Unskip this when you are done building the interpreter")]
    public void GivenBrainfuckInterpreterInBrainfuck_CanRunASimpleProgram()
    {
        var code = @"
        >>>+[[-]>>[-]++>+>+++++++[<++++>>++<-]++>>+>+>+++++[>++>++++++<<-]+>>>,<++[[>[
        ->>]<[>>]<<-]<[<]<+>>[>]>[<+>-[[<+>-]>]<[[[-]<]++<-[<+++++++++>[<->-]>>]>>]]<<
        ]<]<[[<]>[[>]>>[>>]+[<<]<[<]<+>>-]>[>]+[->>]<<<<[[<<]<[<]+<<[+>+<<-[>-->+<<-[>
        +<[>>+<<-]]]>[<+>-]<]++>>-->[>]>>[>>]]<<[>>+<[[<]<]>[[<<]<[<]+[-<+>>-[<<+>++>-
        [<->[<<+>>-]]]<[>+<-]>]>[>]>]>[>>]>>]<<[>>+>>+>>]<<[->>>>>>>>]<<[>.>>>>>>>]<<[
        >->>>>>]<<[>,>>>]<<[>+>]<<[+<<]<]";
        
        // Read 3 chars from input and print them on reading 
        _testInput.SetInput(",.>,.>,.!abc");
        
        _sut.Interpret(code);

        _testOutput.Output.Should().Be("abc");
    }
}