using Database;
using Framework.Build;
using Handlers.Shows;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc()
    .AddApplicationPart(typeof(GetEventsHandler).Assembly);

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        connection,
        x => x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
});

/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
});*/

builder.Services.AddAutoMapper(cfg => { }, typeof(GetEventsHandler));

Builder.FullBuild(builder);