using Dapr.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using WebBff.API.Filters;
using WebBff.API.Interfaces;
using WebBff.API.Services;

namespace WebBff.API
{
    public static class ProgramExtensions
    {
        private const string AppName = "Web Aggregator API";

        public static void AddCustomSerilog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            //.WriteTo.Console()
            //.WriteTo.Seq(seqServerUrl!)
            .Enrich.WithProperty("ApplicationName", AppName)
            .CreateLogger();

            builder.Host.UseSerilog();
        }

        public static void AddCustomSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = $"BrainykatOnDapr - {AppName}", Version = "v1" });

                var identityUrlExternal = builder.Configuration.GetValue<string>("IdentityUrlExternal");

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{identityUrlExternal}/connect/authorize"),
                            TokenUrl = new Uri($"{identityUrlExternal}/connect/token"),
                            Scopes = new Dictionary<string, string>()
                            {
                                { "webaggr-api", AppName }
                            }
                        }
                    }
                });

                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }

        public static void UseCustomSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppName} V1");
                c.OAuthClientId("webaggrswaggerui");
                c.OAuthAppName("Web Aggregator Swagger UI");
            });
        }

        public static void AddCustomAuthentication(this WebApplicationBuilder builder)
        {
            // Prevent mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.Audience = "webaggr-api";
                    options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
                    options.RequireHttpsMetadata = false;
                });
        }

        public static void AddCustomAuthorization(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "webaggr");
                });
            });
        }

        public static void AddCustomHealthChecks(this WebApplicationBuilder builder) =>
            builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDapr()
                .AddUrlGroup(new Uri(builder.Configuration["CustomerUrlHC"]!), name: "customerapi-check", tags: new[] { "customerapi" })
                //.AddUrlGroup(new Uri(builder.Configuration["IdentityUrlHC"]!), name: "identityapi-check", tags: new[] { "identityapi" })
                .AddUrlGroup(new Uri(builder.Configuration["FinanceUrlHC"]!), name: "financeapi-check", tags: new[] { "financeapi" });

        public static void AddCustomApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ICustomerService, CustomerService>(
                _ => new CustomerService(DaprClient.CreateInvokeHttpClient("customers-api")));

            //builder.Services.AddSingleton<IFinanceService, FinanceService>(
            //    _ => new FinanceService(DaprClient.CreateInvokeHttpClient("finance-api")));
        }
    }
}
