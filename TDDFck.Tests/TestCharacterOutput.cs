namespace TDDFck.Tests;

public class TestCharacterOutput : ICharacterOutput
{
    public string Output { get; private set; } = "";
    
    public void OutputChar(char c) => Output += c;
}