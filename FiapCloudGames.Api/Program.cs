using FiapCloudGames.Api.Configurations;
using FiapCloudGames.Api.Profiles;
using FiapCloudGames.Infra.CrossCutting.IoC;
using FiapCloudGames.Infra.Data.Contexts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddDbContext<MySqlContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString(nameof(Database.MySql)), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString(nameof(Database.MySql))));
}, ServiceLifetime.Scoped);

builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddJwtSecurity(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddSingleton<Mapper>();
builder.Services.AddDocumentation();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseDocumentation();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseForwardedHeaders();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireAuthorization();

app.Run();

public partial class Program { }