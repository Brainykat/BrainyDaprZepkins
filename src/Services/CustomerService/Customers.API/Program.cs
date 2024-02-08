using Customers.API;
using Customers.Data;
using HealthChecks.UI.Client;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebHosts.Middleware;

var appName = "Customer API";
var builder = WebApplication.CreateBuilder(args);

//This is for adding migrations
//builder.Services.AddDbContext<CustomersContext>(options =>
//       options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerDB")));

builder.AddCustomConfiguration();
builder.AddCustomSerilog();
builder.AddCustomSwagger();
builder.AddCustomHealthChecks();
builder.AddCustomApplicationServices();
builder.AddCustomDatabase();

builder.Services.AddDaprClient();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCustomSwagger();
}

var pathBase = builder.Configuration["PATH_BASE"];
if (!string.IsNullOrEmpty(pathBase))
{
    app.UsePathBase(pathBase);
}


app.UseCloudEvents();

app.MapGet("/", () => Results.LocalRedirect("~/swagger"));
app.MapControllers();
app.MapSubscribeHandler();
app.MapCustomHealthChecks("/hc", "/liveness", UIResponseWriter.WriteHealthCheckUIResponse);

app.UseMiddleware<RequestLogContextMiddleware>();
app.UseSerilogRequestLogging();
try
{
    app.Logger.LogInformation("Applying database migration ({ApplicationName})...", appName);
    app.ApplyDatabaseMigration();

    app.Logger.LogInformation("Starting web host ({ApplicationName})...", appName);
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Host terminated unexpectedly ({ApplicationName})...", appName);
}
finally
{
    Serilog.Log.CloseAndFlush();
}
