using Application;
using Infrastructure;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Services.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// appsettings.json
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.local.json")
    .AddJsonFile("appsettings.json");

builder.Services.AddHttpClient();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddSingleton<IJobFactory, JobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddSingleton<JobRunner>();
builder.Services.AddHostedService<HostedService>();
builder.Services.AddScoped<SendExchangeRateInfoJob>();
builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(SendExchangeRateInfoJob)));

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
