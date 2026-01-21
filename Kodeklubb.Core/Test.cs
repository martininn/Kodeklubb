namespace Kodeklubb.Core;

public class Test
{
    public Test()
    {
        var test3 = new Test3();
        test3.FirstName = "Sjef";
    }
}

public struct Test2
{
    public Test2()
    {
        
    }
}

public record struct Test3
{
    public string FirstName;
    public string LastName;
    public Test3()
    {
        
    }
}

public record Test4
{
    public string Sjef;

    public Test4()
    {
        
    }
}