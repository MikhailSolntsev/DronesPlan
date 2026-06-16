using DronesPlan.DAL;
using DronesPlan.DAL.Repositories;
using DronesPlan.Domain.Interfaces;
using DronesPlan.Domain.Services;
using DronesPlan.Domain.Configuration;
using DronesPlan.Infrastructure.Clients;
using DronesPlan.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DronesPlanDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISnapshotRepository, SnapshotRepository>();
builder.Services.AddScoped<ITrackedIssueRepository, TrackedIssueRepository>();
builder.Services.AddScoped<IPlannedAssignmentRepository, PlannedAssignmentRepository>();

builder.Services.Configure<StatusMappingOptions>(builder.Configuration.GetSection("StatusMapping"));
builder.Services.AddScoped<ISnapshotService, SnapshotService>();

builder.Services.Configure<JiraOptions>(builder.Configuration.GetSection(JiraOptions.SectionName));
builder.Services.AddHttpClient<IJiraClient, JiraClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapGet("/api/status", () => Results.Ok(new { Status = "Running" }));

app.Run();
