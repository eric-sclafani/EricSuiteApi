using EricSuiteApi.Features.Migraines.Repositories;
using EricSuiteApi.Features.Migraines.Services;
using EricSuiteApi.Infrastructure.Cosmos;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCosmos(builder.Configuration);

builder.Services.AddScoped<IMigraineRepository, MigraineRepository>();
builder.Services.AddScoped<MigrainesService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();