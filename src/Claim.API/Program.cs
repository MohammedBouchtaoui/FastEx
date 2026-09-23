var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
// Dans Program.cs (avant builder.Build())
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Après builder.Build()
app.UseCors("AllowReact");


var claims = new List<Claim>();

// POST: Déclarer un sinistre
app.MapPost("/api/claims", async (CreateClaimDto dto, IHttpClientFactory httpClientFactory, IConfiguration config) =>
{
    var claim = new Claim(
        Id: Guid.NewGuid(),
        PolicyNumber: dto.PolicyNumber,
        Description: dto.Description,
        Status: "Submitted",
        CreatedAt: DateTime.UtcNow
    );
    claims.Add(claim);

    // Notification HTTP vers Expertise.API
    var expertiseServiceUrl = config["Services:ExpertiseApi"] ?? "http://localhost:5002";
    var client = httpClientFactory.CreateClient();
    var expertiseRequest = new { ClaimId = claim.Id, Instructions = "Expertise rapide FastEx requise." };

    try
    {
        await client.PostAsJsonAsync($"{expertiseServiceUrl}/api/expertises", expertiseRequest);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[FastEx WARNING] Échec de communication avec Expertise.API: {ex.Message}");
    }

    return Results.Created($"/api/claims/{claim.Id}", claim);
});

// GET: Lister les sinistres
app.MapGet("/api/claims", () => Results.Ok(claims));

// Endpoint de santé pour Kubernetes
app.MapGet("/healthz", () => Results.Ok("Claim.API FastEx is healthy"));

app.Run();

record Claim(Guid Id, string PolicyNumber, string Description, string Status, DateTime CreatedAt);
record CreateClaimDto(string PolicyNumber, string Description);