using Api.Endpoints;
using Api.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("escuela_db");
builder.Services.AddDbContext<EscuelaContext>(options => options.UseNpgsql(connectionString));
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    options.EnableTryItOutByDefault()
    );
}

app.MapGroup("/api")
    .MapUsuarioEndpoints()
    .WithTags("Usuario");

app.MapGroup("/api")
    .MapRolEndpoints()
    .WithTags("Rol");

app.Run();
