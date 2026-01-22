namespace Kodeklubb.Core;

public record Result(
    Outcome Outcome,
    TeamState NewState,
    IReadOnlyList<IDomainEvent> Events
    );