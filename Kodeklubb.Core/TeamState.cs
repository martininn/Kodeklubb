namespace Kodeklubb.Core;

public record TeamState(
    Guid TeamId,
    IReadOnlyList<Guid> Members,
    IReadOnlyList<Guid> PendingInvitations
    );