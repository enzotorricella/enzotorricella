// Original demonstration: invented data, no commercial source code.
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var incidents = new ConcurrentDictionary<Guid, Incident>();
var seed = new Incident(Guid.NewGuid(), "Demo: printing error", "Open", "Medium", DateTimeOffset.UtcNow);
incidents[seed.Id] = seed;

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapGet("/incidents", (string? status) =>
{
    var items = incidents.Values.Where(i => status is null ||
        i.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
        .OrderByDescending(i => i.CreatedAt);
    return Results.Ok(items);
});
app.MapGet("/incidents/{id:guid}", (Guid id) =>
    incidents.TryGetValue(id, out var incident) ? Results.Ok(incident) : Results.NotFound());
app.MapPost("/incidents", (CreateIncident request) =>
{
    if (string.IsNullOrWhiteSpace(request.Title) || request.Title.Length > 120)
        return Results.BadRequest(new { error = "Title must be 1–120 characters." });
    if (!new[] { "Low", "Medium", "High" }.Contains(request.Priority, StringComparer.OrdinalIgnoreCase))
        return Results.BadRequest(new { error = "Invalid priority." });

    var incident = new Incident(Guid.NewGuid(), request.Title.Trim(), "Open",
        request.Priority, DateTimeOffset.UtcNow);
    incidents[incident.Id] = incident;
    return Results.Created($"/incidents/{incident.Id}", incident);
});
app.MapPatch("/incidents/{id:guid}/status", (Guid id, UpdateStatus request) =>
{
    if (!new[] { "Open", "Investigating", "Resolved" }.Contains(request.Status, StringComparer.OrdinalIgnoreCase))
        return Results.BadRequest(new { error = "Invalid status." });
    if (!incidents.TryGetValue(id, out var existing)) return Results.NotFound();
    var updated = existing with { Status = request.Status };
    incidents[id] = updated;
    return Results.Ok(updated);
});

app.Run();

record Incident(Guid Id, string Title, string Status, string Priority, DateTimeOffset CreatedAt);
record CreateIncident(string Title, string Priority);
record UpdateStatus(string Status);
