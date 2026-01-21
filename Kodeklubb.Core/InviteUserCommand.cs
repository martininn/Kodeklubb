namespace Kodeklubb.Core;

public record InviteUserCommand(
    Guid TeamId,
    Guid InvitedUserId,
    Guid InvitedByUserId
    );