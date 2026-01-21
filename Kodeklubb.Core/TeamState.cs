namespace Kodeklubb.Core;

public record TeamState(
    Guid TeamId,
    List<Guid> Members,
    List<Guid> PendingInvitations
    );