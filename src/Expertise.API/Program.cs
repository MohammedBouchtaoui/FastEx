var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var expertises = new List<ExpertiseMission>();

// POST: Créer une mission d'expertise
app.MapPost("/api/expertises", (CreateExpertiseDto dto) =>
{
    var mission = new ExpertiseMission(
        Id: Guid.NewGuid(),
        ClaimId: dto.ClaimId,
        Instructions: dto.Instructions,
        EstimatedAmount: 0,
        Status: "Assigned"
    );
    expertises.Add(mission);

    Console.WriteLine($"[FastEx INFO] Mission d'expertise créée pour le sinistre {dto.ClaimId}");
    return Results.Created($"/api/expertises/{mission.Id}", mission);
});

// GET: Lister les missions d'expertise
app.MapGet("/api/expertises", () => Results.Ok(expertises));

// Endpoint de santé pour Kubernetes
app.MapGet("/healthz", () => Results.Ok("Expertise.API FastEx is healthy"));

app.Run();

record ExpertiseMission(Guid Id, Guid ClaimId, string Instructions, decimal EstimatedAmount, string Status);
record CreateExpertiseDto(Guid ClaimId, string Instructions);