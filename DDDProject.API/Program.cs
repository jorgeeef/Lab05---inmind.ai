using System.Globalization;
using DDD.Persistence;
using DDDProject.Application.Services;
using DDDProject.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using DDD.Persistence.Repositories;
using DDDProject.API.Middlewares;
using DDDProject.Domain.Repositories;
using DDDProject.Infrastructure.Localization;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.PostgreSql;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/ums-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResource));
    });

var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("fr")
};

// Add middleware to the app builder pipeline


builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure();
builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();
builder.Services.AddApplication();


builder.Services.AddHangfire(config =>
{
    config.UsePostgreSqlStorage("Host=localhost;Database=UMS;Username=user;Password=user");
});
builder.Services.AddHangfireServer();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379"; 
    options.InstanceName = "DDDProject_";
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCultureMiddleware();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();