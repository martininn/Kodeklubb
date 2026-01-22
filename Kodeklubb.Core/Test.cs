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
}

public record struct Test3
{
    public string FirstName;
    public string LastName;
}

public record Test4
{
    public string Sjef { get; init; }

    public Test4(string sjef)
    {
        Sjef = sjef;
    }
}

public class Test5(string sjef, int bolle);

public class A {}
public record B {}
public struct C {}
public record struct D {}