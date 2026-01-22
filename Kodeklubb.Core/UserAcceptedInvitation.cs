namespace Kodeklubb.Core;

public record UserAcceptedInvitation(
    Guid TeamId,
    Guid InvitedUserId,
    DateTime AcceptedAt
    ) : IDomainEvent
{
    public DateTime TimeStamp { get; } = AcceptedAt;
}