namespace Claim.API.Controllers;

using Claim.API.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClaimsController : ControllerBase
{
    private readonly IClaimService _claimService;

    public ClaimsController(IClaimService claimService)
    {
        _claimService = claimService;
    }

    [HttpGet]
    public async Task GetAll()
    {
        var claims = await _claimService.GetClaimsAsync();
        return Ok(claims);
    }

    [HttpPost]
    public async Task Create([FromBody] CreateClaimDto dto)
    {
        var result = await _claimService.CreateClaimAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }
}