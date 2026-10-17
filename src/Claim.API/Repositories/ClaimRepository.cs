namespace Claim.API.Repositories;

using Claim.API.Models;
using System.Collections.Concurrent;

public interface IClaimRepository
{
    Task> GetAllAsync();
    Task GetByIdAsync(Guid id);
    Task AddAsync(Claim claim);
}

public class InMemClaimRepository : IClaimRepository
{
    private readonly ConcurrentDictionary _claims = new();

    public Task> GetAllAsync()
        => Task.FromResult(_claims.Values.AsEnumerable());

    public Task GetByIdAsync(Guid id)
        => Task.FromResult(_claims.TryGetValue(id, out var claim) ? claim : null);

    public Task AddAsync(Claim claim)
    {
        _claims.TryAdd(claim.Id, claim);
        return Task.CompletedTask;
    }
}