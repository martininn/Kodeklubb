namespace Kodeklubb.Core;

public interface IDomainEvent
{
    public DateTime TimeStamp { get; }
}