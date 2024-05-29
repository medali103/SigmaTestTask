using Microsoft.EntityFrameworkCore;
using SigmaTestTask.Repositories;
using SigmaTestTask.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the DbContext
builder.Services.AddDbContext<CandidateContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();

// Register services
builder.Services.AddScoped<ICandidateService, CandidateService>();

// Add caching
builder.Services.AddMemoryCache();

// If you are using the CachedCandidateService, register it here
// builder.Services.Decorate<ICandidateService, CachedCandidateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
