namespace Kodeklubb.Core;

public record AcceptInvitationCommand(
    Guid TeamId,
    Guid InvitedUserId
    );