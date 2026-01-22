namespace Kodeklubb.Core;

public record UserInvitedToTeam(
    Guid TeamId,
    Guid InvitedUserId,
    DateTime InvitedAt
    ) : IDomainEvent
{
    public DateTime TimeStamp { get; } = InvitedAt;
}