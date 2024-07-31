namespace TDDFck.Interpreter;

public class BrainFuckInterpreter : IBrainFuckInterpreter
{
    private byte[] _memory;

    private readonly ICharacterInput _input;
    private readonly ICharacterOutput _output;

    public IReadOnlyCollection<byte> Memory => _memory;

    public BrainFuckInterpreter(uint memorySize, ICharacterInput input, ICharacterOutput output)
    {
        _memory = new byte[memorySize];
        _input = input;
        _output = output;
    }

    /// <summary>
    /// Based on the specification from http://brainfuck.org/brainfuck.html
    /// </summary>
    /// <remarks>
    /// This interpreter makes the following assumptions:
    /// <list type="bullet">
    ///     <item> Cells are 8 bits / a byte long </item>
    ///     <item> Cells can under/over-flow (0 - 1 becomes 255, 255 + 1 becomes 0) </item>
    ///     <item> The pointer wraps around to the beginning/end of the memory if it goes out of bounds </item>
    /// </list>
    /// This interpreter does not handle:
    /// <list type="bullet">
    ///     <item> Unmatched '[' or ']' in the program </item>
    ///     <item> Dynamic memory length </item>
    ///     <item> Stopping EOF from input </item>
    /// </list>
    /// </remarks>
    public void Interpret(string code)
    {
        // TODO: Implement this - You should only need to touch this file (and unskip tests)
        throw new NotImplementedException();
    }
}