namespace Kodeklubb.Core;

public record TeamService()
{
    public static Result Handle(
        TeamState state, 
        InviteUserCommand command, 
        DateTime now)
    {
        if (!state.Members.Contains(command.InvitedByUserId))
        {
            return new Result(
                new Outcome(OutcomeStatus.Rejected, "NotMember"),
                state,
                new List<IDomainEvent>()
            );
        }
        if (state.Members.Contains(command.InvitedUserId))
        {
            return new Result(
                new Outcome(OutcomeStatus.Rejected, "AlreadyMember"),
                state,
                new List<IDomainEvent>()
            );
        }
        if (state.PendingInvitations.Contains(command.InvitedUserId))
        {
            return new Result(
                new Outcome(OutcomeStatus.Rejected, "AlreadyInvited"),
                state,
                new List<IDomainEvent>()
            );
        }
        var newPendingInvitations = state.PendingInvitations
            .Append(command.InvitedUserId)
            .ToList();
        TeamState newState = state with
        {
            PendingInvitations = newPendingInvitations
        };
        return new Result(
            Outcome.Accepted(),
            newState,
            new List<IDomainEvent>
            {
                new UserInvitedToTeam(state.TeamId, command.InvitedUserId, now)
            }
        );
    }
}