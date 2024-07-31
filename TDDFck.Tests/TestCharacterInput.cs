namespace TDDFck.Tests;

public class TestCharacterInput : ICharacterInput
{
    private string _input = "";
    private int _index = 0;

    public void SetInput(string input)
    {
        _input = input;
        _index = 0;
    }

    public char GetNextChar() => _input[_index++];
}