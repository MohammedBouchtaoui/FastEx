namespace Expertise.API.Models;

public class ExpertiseMission
{
    public Guid Id { get; private set; }
    public string ClaimReference { get; private set; }
    public string Status { get; private set; }
    public DateTime AssignedAt { get; private set; }

    public ExpertiseMission(string claimReference)
    {
        Id = Guid.NewGuid();
        ClaimReference = claimReference;
        Status = "Assigned";
        AssignedAt = DateTime.UtcNow;
    }
}