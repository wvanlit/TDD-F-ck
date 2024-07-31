namespace TDDFck.Interpreter;

public class BrainFuckInterpreter : IBrainFuckInterpreter
{
    private byte[] _memory;
    private uint _memoryPointer;
    private int _programPointer;
    
    private readonly ICharacterInput _input;
    private readonly ICharacterOutput _output;

    public IReadOnlyCollection<byte> Memory => _memory;

    public BrainFuckInterpreter(uint memorySize, ICharacterInput input, ICharacterOutput output)
    {
        _memory = new byte[memorySize];
        _input = input;
        _output = output;
    }

    public void Interpret(string code)
    {
        while (_programPointer < code.Length)
        {
            switch (code[_programPointer++])
            {
                case '+':
                    _memory[_memoryPointer] += 1;
                    break;
                case '-':
                    _memory[_memoryPointer] -= 1;
                    break;
                case '>':
                    _memoryPointer = _memoryPointer + 1 < Memory.Count ? _memoryPointer + 1 : 0;
                    break;
                case '<':
                    _memoryPointer = Math.Min(_memoryPointer - 1, (uint)Memory.Count - 1);
                    break;
                case ',':
                    _memory[_memoryPointer] = (byte)_input.GetNextChar();
                    break;
                case '.':
                    _output.OutputChar((char) _memory[_memoryPointer]);
                    break;
                case '[':
                    break;
                case ']':
                    if (_memory[_memoryPointer] != 0)
                        _programPointer = code.IndexOf('[');
                    break;
            }
        }
    }
}