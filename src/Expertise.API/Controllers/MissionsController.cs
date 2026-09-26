namespace Expertise.API.Controllers;

using Expertise.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

[ApiController]
[Route("api/[controller]")]
public class MissionsController : ControllerBase
{
    private static readonly ConcurrentBag Missions = new()
    {
        new ExpertiseMission("1082b4c5-a39e-4d47-bfa1-2023a34d4682")
    };

    [HttpGet]
    public IActionResult GetAll() => Ok(Missions);

    [HttpPost]
    public IActionResult Create([FromBody] string claimReference)
    {
        var mission = new ExpertiseMission(claimReference);
        Missions.Add(mission);
        return Created("", mission);
    }
}