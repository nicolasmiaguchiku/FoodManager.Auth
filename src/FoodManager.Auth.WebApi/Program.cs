using FoodManager.Auth.CrossCutting.Extentions;
using FoodManager.Internal.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);
var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{enviroment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var applicationSettings = builder.Configuration.ApplyEnvironmentOverridesToSettings(builder.Environment);

builder.Services
    //REFATORAR EXTENSION METHOD
    //.AddHttpClients(applicationSettings.KeycloakSettings)
    .AddRepositories(applicationSettings)
    .AddMongo(applicationSettings.MongoSettings)
    .ConfigureValidationErrorResponses()
    .AddApiSpecification()
    .ConfigureLiteBus()
    .AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Host.UseSerilog(enviroment!, applicationSettings.MltSettings.SeqUrl!);

var app = builder.Build();

app.MapOpenApi();
app.UseSpecification("Auth");

app.UseRequestContextLogging()
   .UseHttpsRedirection()
   .UseAuthentication()
   .UseAuthorization();

app.MapControllers();

await app.RunAsync();