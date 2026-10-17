namespace Claim.API.Models;

public enum ClaimStatus
{
    Submitted,
    InReview,
    Approved,
    Rejected
}

public class Claim
{
    public Guid Id { get; private set; }
    public string PolicyNumber { get; private set; }
    public string DamageDescription { get; private set; }
    public ClaimStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Claim(string policyNumber, string damageDescription)
    {
        if (string.IsNullOrWhiteSpace(policyNumber))
            throw new ArgumentException("Le numéro de police est obligatoire.");

        Id = Guid.NewGuid();
        PolicyNumber = policyNumber;
        DamageDescription = damageDescription;
        Status = ClaimStatus.Submitted;
        CreatedAt = DateTime.UtcNow;
    }
}