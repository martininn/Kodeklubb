namespace Kodeklubb.Core;

public record Result(
    Outcome Outcome,
    TeamState NewState,
    List<IDomainEvent> Events
    );