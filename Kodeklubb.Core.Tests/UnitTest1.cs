namespace Kodeklubb.Core.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void InviterPutsUserInPendingAndReturnsEvent()
    {
        var teamId = Guid.NewGuid();
        var invitedByUserId = Guid.NewGuid();
        var invitedUserId = Guid.NewGuid();

        var inviteCommand = new InviteUserCommand(teamId, invitedUserId, invitedByUserId);

        Console.WriteLine(inviteCommand);
        
        var teamState = new TeamState(teamId, new List<Guid>{ invitedByUserId }, new List<Guid>{  });
        var command = new InviteUserCommand(teamState.TeamId, invitedUserId, invitedByUserId);
        var now = new DateTime(2026, 01, 21);
        
        var result = TeamService.Handle(teamState, command, now);
        Assert.That(result.NewState.PendingInvitations, Contains.Item(invitedUserId));
        Assert.That(result.Events.Count, Is.EqualTo(1));
        Assert.That(result.Outcome.Status, Is.EqualTo(OutcomeStatus.Accepted));
        Console.WriteLine(result.Outcome);
    }

    [Test]
    public void InviterIsNotAMember()
    {
        var teamId = Guid.NewGuid();
        var invitedByUserId = Guid.NewGuid();
        var invitedUserId = Guid.NewGuid();
        
        var teamState = new TeamState(teamId, new List<Guid>{  }, new List<Guid>{  });
        var command = new InviteUserCommand(teamState.TeamId, invitedUserId, invitedByUserId);
        var now = new DateTime(2026, 01, 21);
        
        var result = TeamService.Handle(teamState, command, now);
        Assert.That(result.Outcome.Status, Is.EqualTo(OutcomeStatus.Rejected));
        Console.WriteLine(result.Outcome);
    }

    [Test]
    public void InvitedUserIsAlreadyAMember()
    {
        var teamId = Guid.NewGuid();
        var invitedByUserId = Guid.NewGuid();
        var invitedUserId = Guid.NewGuid();
        
        var teamState = new TeamState(teamId, new List<Guid>{ invitedUserId, invitedByUserId }, new List<Guid>{  });
        var command = new InviteUserCommand(teamState.TeamId, invitedUserId, invitedByUserId);
        var now = new DateTime(2026, 01, 21);
        
        var result = TeamService.Handle(teamState, command, now);
        Assert.That(result.Outcome.Status, Is.EqualTo(OutcomeStatus.Rejected));
        Console.WriteLine(result.Outcome);
    }

    [Test]
    public void InvitedUserIsAlreadyInvited()
    {
        var teamId = Guid.NewGuid();
        var invitedByUserId = Guid.NewGuid();
        var invitedUserId = Guid.NewGuid();
        
        var teamState = new TeamState(teamId, new List<Guid>{ invitedByUserId }, new List<Guid>{ invitedUserId });
        var command = new InviteUserCommand(teamState.TeamId, invitedUserId, invitedByUserId);
        var now = new DateTime(2026, 01, 21);
        
        var result = TeamService.Handle(teamState, command, now);
        Assert.That(result.Outcome.Status, Is.EqualTo(OutcomeStatus.Rejected));
        Console.WriteLine(result.Outcome);
    }

    [Test]
    public void UserAcceptsInvitation()
    {
        var teamId = Guid.NewGuid();
        var invitedUserId = Guid.NewGuid();

        var teamState = new TeamState(teamId, new List<Guid>(), new List<Guid> { invitedUserId });
        var command = new AcceptInvitationCommand(teamId, invitedUserId);
        var now = new DateTime(2026, 01, 22);

        var result = TeamService.HandleAccepts(teamState, command, now);
        Assert.That(result.Outcome.Status, Is.EqualTo(OutcomeStatus.Accepted));
        Console.WriteLine(result.Outcome);
    }
}