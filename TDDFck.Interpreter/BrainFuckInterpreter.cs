﻿namespace TDDFck.Interpreter;

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

    public void Interpret(string code)
    {
        throw new NotImplementedException();
    }
}