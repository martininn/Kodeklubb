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

    public static Result HandleAccepts(
        TeamState state, 
        AcceptInvitationCommand command, 
        DateTime now)
    {
        if (!state.PendingInvitations.Contains(command.InvitedUserId))
        {
            return new Result(
                new Outcome(OutcomeStatus.Rejected, "User does not exist in pending invitations."),
                state,
                new List<IDomainEvent>()
            );
        }

        if (state.Members.Contains(command.InvitedUserId))
        {
            return new Result(
                new Outcome(OutcomeStatus.Rejected, "User already is a member."),
                state,
                new List<IDomainEvent>()
            );
        }

        var newMembers = state.Members
            .Append(command.InvitedUserId)
            .ToList();
        var newPendingInvitations = state.PendingInvitations.Where(id => id != command.InvitedUserId).ToList();
        var newState = state with
        {
            Members = newMembers,
            PendingInvitations = newPendingInvitations
        };

        return new Result(
            new Outcome(OutcomeStatus.Accepted, "AcceptedInvitation"),
            newState,
            new List<IDomainEvent>{ new UserAcceptedInvitation(state.TeamId, command.InvitedUserId, now)} 
        );
    }
}