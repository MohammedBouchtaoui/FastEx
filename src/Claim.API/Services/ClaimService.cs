namespace Claim.API.Services;

using Claim.API.Models;
using Claim.API.Repositories;

public record CreateClaimDto(string PolicyNumber, string DamageDescription);

public interface IClaimService
{
    Task> GetClaimsAsync();
    Task CreateClaimAsync(CreateClaimDto dto);
}

public class ClaimService : IClaimService
{
    private readonly IClaimRepository _repository;

    public ClaimService(IClaimRepository repository)
    {
        _repository = repository;
    }

    public async Task> GetClaimsAsync()
        => await _repository.GetAllAsync();

    public async Task CreateClaimAsync(CreateClaimDto dto)
    {
        var claim = new Claim(dto.PolicyNumber, dto.DamageDescription);
        await _repository.AddAsync(claim);
        return claim;
    }
}