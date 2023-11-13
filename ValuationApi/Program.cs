using FluentValidation;
using ValuationApi.Models;
using ValuationApi.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddLogging();

// Memory cache
builder.Services.AddMemoryCache();

// Add services to the container.
builder.Services.AddScoped<IVesselRepository, VesselRepository>();
builder.Services.AddScoped<IValuationRepository, ValuationRepository>();
builder.Services.AddScoped<ICoefficientRepository, CoefficientRepository>();

// Validator
builder.Services.AddScoped<IValidator<Vessel>, VesselValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
