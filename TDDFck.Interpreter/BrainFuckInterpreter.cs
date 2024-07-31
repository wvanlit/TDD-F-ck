namespace TDDFck.Interpreter;

public class BrainFuckInterpreter : IBrainFuckInterpreter
{
    private uint[] _memory;
    
    private readonly ICharacterInput _input;
    private readonly ICharacterOutput _output;

    public IReadOnlyCollection<uint> Memory => _memory;

    public BrainFuckInterpreter(uint memorySize, ICharacterInput input, ICharacterOutput output)
    {
        _memory = new uint[memorySize];
        _input = input;
        _output = output;
    }

    public void Interpret(string code)
    {
        _memory[0] = (uint)code.Count(c => c is '+');
    }
}