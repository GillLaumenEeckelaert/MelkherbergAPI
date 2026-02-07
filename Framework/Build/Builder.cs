using Framework.Database;
using Framework.Extensions;
using Framework.Handlers;
using Framework.Handlers.Permissions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Framework.Build;

public static class Builder
{
    public static WebApplicationBuilder BuildServices(
        WebApplicationBuilder builder
        )
    {
        // Add framework DB Context
        builder.Services.AddDbContext<FrameworkDbContext>(options =>
        {
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(typeof(FrameworkDbContext).Assembly.FullName));
        });
        
        // Add controllers
        builder.Services.AddControllers(opts =>
        {
            opts.Conventions.Add(new CustomRouteUrl());
        });
        
        // Add handlers
        builder.Services.AddMvc()
            .AddApplicationPart(typeof(GetPermissionsHandler).Assembly);
        builder.Services.AddAutoMapper(cfg => { }, typeof(GetPermissionsHandler));
        
        // Add Identity
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
        builder.Services.AddIdentityApiEndpoints<IdentityUser>()
            .AddEntityFrameworkStores<FrameworkDbContext>();
        
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwaggerGen(options =>
        {
            options.OperationFilter<AddHeadersFilter>();
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Melkherberg Server API",
                Description = "De API voor de Melkherberg server."
            });
            options.TagActionsBy(api =>
            {
                if (api.GroupName != null)
                {
                    return [api.GroupName];
                }

                if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                {
                    var namespaceElements = controllerActionDescriptor.ControllerTypeInfo.ToString().Split('.');
                    return [namespaceElements.ElementAt(namespaceElements.Length - 2)];
                }

                return ["Other"];
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    []
                }
            });
        });

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
        });
        
        return builder;
    }

    public static void Build(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }
        
        //app.MapIdentityApi<IdentityUser>();
        app.MapCustomizedIdentityApi<IdentityUser>();
        app.MapAuthenticationIdentityApi<IdentityUser>();

        app.UseHttpsRedirection();

        app.UseCors();

        app.MapControllers();

        app.Run();
    }

    public static void FullBuild(WebApplicationBuilder builder)
    {
        var fwBuilder = BuildServices(builder);
        var app = fwBuilder.Build();
        Build(app);
    }
}