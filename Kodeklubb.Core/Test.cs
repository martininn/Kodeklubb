namespace Kodeklubb.Core;

public class Test
{
    public string Sjef { get; init; }
    public Test(string sjef)
    {
        Sjef = sjef;
    }
}

public record Test2(string Sjef);

public readonly record struct Test3(string sjef);


public readonly struct Test4(string sjef);

public class Test5(string sjef, int bolle);

public class A {}
public record B {}
public struct C {}
public record struct D {}