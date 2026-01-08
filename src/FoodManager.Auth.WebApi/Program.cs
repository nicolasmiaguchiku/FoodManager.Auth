using FoodManager.Auth.CrossCutting.Extentions;
using FoodManager.Internal.Shared.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{enviroment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var applicationSettings = builder.Configuration.ApplyEnvironmentOverridesToSettings(builder.Environment);

builder.Services
    .AddHttpClients(applicationSettings.KeycloakSettings)
    .AddRepositories(applicationSettings)
    .AddApiAuthentication(applicationSettings.KeycloakSettings.Realm)
    .ConfigureLiteBus()
    .AddOpenApi("v1");

builder.Services.AddControllers();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(options => options.Servers = []);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();