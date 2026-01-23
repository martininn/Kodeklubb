namespace Kodeklubb.Core.Tests;

public class Tests
{
    [TestFixture]
    public class TeamServiceTests
    {
        private Guid _teamId;
        private Guid _invitedByUserId;
        private Guid _invitedUserId;
        private DateTime _now;
        
        [SetUp]
        public void Setup()
        {
            _teamId = Guid.NewGuid();
            _invitedByUserId = Guid.NewGuid();
            _invitedUserId = Guid.NewGuid();
            _now = new DateTime(2026, 01, 23);
        }
        
        [Test]
        public void InviterPutsUserInPendingAndReturnsEvent()
        {
            var teamState = new TeamState(_teamId, new List<Guid>{ _invitedByUserId }, new List<Guid>{  });
            var command = new InviteUserCommand(teamState.TeamId, _invitedUserId, _invitedByUserId);
            
            var result = TeamService.Handle(teamState, command, _now);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.NewState.PendingInvitations, Contains.Item(_invitedUserId));
                Assert.That(result.Events, Has.Count.EqualTo(1));
                Assert.That(result.Outcome.Status, Is.EqualTo(OutcomeStatus.Accepted));
            }
            Console.WriteLine(result.Outcome);
        }

        [Test]
        public void InviterIsNotAMember()
        {
            var teamState = new TeamState(_teamId, new List<Guid>{  }, new List<Guid>{  });
            var command = new InviteUserCommand(teamState.TeamId, _invitedUserId, _invitedByUserId);
            
            var result = TeamService.Handle(teamState, command, _now);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.NewState.Members, Does.Not.Contain(_invitedByUserId));
                Assert.That(result.Outcome.Status, Is.EqualTo(OutcomeStatus.Rejected));
                Assert.That(result.Events, Is.Empty);
            }
            Console.WriteLine(result.Outcome);
        }

        [Test]
        public void InvitedUserIsAlreadyAMember()
        {
            var teamState = new TeamState(_teamId, new List<Guid>{ _invitedUserId, _invitedByUserId }, new List<Guid>{  });
            var command = new InviteUserCommand(teamState.TeamId, _invitedUserId, _invitedByUserId);
            
            var result = TeamService.Handle(teamState, command, _now);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Outcome.Status, Is.EqualTo(OutcomeStatus.Rejected));
                Assert.That(result.Events, Is.Empty);
            }
            Console.WriteLine(result.Outcome);
        }

        [Test]
        public void InvitedUserIsAlreadyInvited()
        {
            var teamState = new TeamState(_teamId, new List<Guid>{ _invitedByUserId }, new List<Guid>{ _invitedUserId });
            var command = new InviteUserCommand(teamState.TeamId, _invitedUserId, _invitedByUserId);
            
            var result = TeamService.Handle(teamState, command, _now);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.NewState.Members, Does.Contain(_invitedByUserId));
                Assert.That(result.NewState.PendingInvitations, Does.Contain(_invitedUserId));
                Assert.That(result.Outcome.Status, Is.EqualTo(OutcomeStatus.Rejected));
            }
            Console.WriteLine(result.Outcome);
        }
        
        [Test]
        public void StateIsUnchangedIfInvitationIsRejected_EventListShouldBeEmpty()
        {
            var teamState = new TeamState(_teamId, new List<Guid>{ _invitedByUserId }, new List<Guid>{ _invitedUserId });
            var command = new InviteUserCommand(teamState.TeamId, _invitedUserId, _invitedByUserId);
            
            var result = TeamService.Handle(teamState, command, _now);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.NewState.Members, Contains.Item(_invitedByUserId));
                Assert.That(result.NewState.Members, Has.Count.EqualTo(1));
                Assert.That(result.NewState.PendingInvitations, Has.Count.EqualTo(1));
                Assert.That(result.NewState.PendingInvitations, Does.Contain(_invitedUserId));
                Assert.That(result.Events, Is.Empty);
            }
            
        }

        [Test]
        public void UserAcceptsInvitation()
        {
            var teamState = new TeamState(_teamId, new List<Guid>(), new List<Guid> { _invitedUserId });
            var command = new AcceptInvitationCommand(_teamId, _invitedUserId);

            var result = TeamService.HandleAccepts(teamState, command, _now);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.NewState.Members, Has.Count.EqualTo(1));
                Assert.That(result.NewState.Members, Does.Contain(_invitedUserId));
                Assert.That(result.NewState.PendingInvitations, Is.Empty);
                Assert.That(result.Outcome.Status, Is.EqualTo(OutcomeStatus.Accepted));
            }
            Console.WriteLine(result.Outcome);
        }
    }
}