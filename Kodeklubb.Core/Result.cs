namespace Kodeklubb.Core;

public readonly record struct Result(
    Outcome Outcome,
    TeamState NewState,
    IReadOnlyList<IDomainEvent> Events
    );