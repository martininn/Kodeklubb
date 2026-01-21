namespace Kodeklubb.Core;

public enum OutcomeStatus
{
    Accepted,
    Rejected
}

public record Outcome(
    OutcomeStatus Status,
    string? ErrorCode = null
)
{
    public static Outcome Accepted()
    {
        return new Outcome(OutcomeStatus.Accepted);
    }

    public static Outcome Rejected(string message)
    {
        return new Outcome(OutcomeStatus.Rejected, "User is already a member");
    }
}