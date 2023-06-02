using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using TODOList.Domain;
using TODOListUniProject.Api.Configuration;
using TODOListUniProject.Api.Validation;
using TODOListUniProject.Contracts.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((ctx, lc) => 
{
    lc.ReadFrom.Configuration(builder.Configuration);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items"
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddHealthChecks().AddNpgSql((sp) => 
{
    var configuration = sp.GetRequiredService<IOptionsMonitor<AppConfiguration>>();
    return configuration.CurrentValue.ConnectionString;
},timeout: TimeSpan.FromSeconds(2));



//new
builder.Services.Configure<AppConfiguration>(builder.Configuration);
builder.Services.AddDomainServices((sp, options) => 
    {
        var configuration = sp.GetRequiredService<IOptionsMonitor<AppConfiguration>>();
        var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
        options.UseNpgsql(configuration.CurrentValue.ConnectionString).UseLoggerFactory(loggerFactory);
    });

//validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<CreateObjectiveRequest>, CreateObjectiveRequestVaidation>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

//app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
public partial class Program { }