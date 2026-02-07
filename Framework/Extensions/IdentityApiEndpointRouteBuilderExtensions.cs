// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IEndpointRouteBuilder"/> to add identity endpoints.
/// </summary>
public static class IdentityApiEndpointRouteBuilderExtensions
{
    public static IEndpointConventionBuilder MapCustomizedIdentityApi<TUser>(this IEndpointRouteBuilder endpoints)
        where TUser : class, new()
    {
        ArgumentNullException.ThrowIfNull(endpoints);
        
        var routeGroup = endpoints.MapGroup("Dev");

        routeGroup.MapPost("/SwaggerLogin", async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>>
            ([FromServices] IServiceProvider sp) =>
        {
            var signInManager = sp.GetRequiredService<SignInManager<TUser>>();

            signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;

            var result = await signInManager.PasswordSignInAsync("gill.eeckelaert@telenet.be", "@Whe2bdB3A", false, lockoutOnFailure: true);
            
            if (!result.Succeeded)
            {
                return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            }

            return TypedResults.Empty;
        });

        return new IdentityEndpointsConventionBuilder(routeGroup);
    }
    
    public static IEndpointConventionBuilder MapAuthenticationIdentityApi<TUser>(this IEndpointRouteBuilder endpoints)
        where TUser : class, new()
    {
        ArgumentNullException.ThrowIfNull(endpoints);
        
        var routeGroup = endpoints.MapGroup("Auth");

        routeGroup.MapPost("/Login", async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>>
            ([FromBody] LoginRequest login, [FromServices] IServiceProvider sp) =>
        {
            var signInManager = sp.GetRequiredService<SignInManager<TUser>>();

            signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;

            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: true);
            
            if (!result.Succeeded)
            {
                return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            }

            return TypedResults.Empty;
        });

        return new IdentityEndpointsConventionBuilder(routeGroup);
    }
    
    // Wrap RouteGroupBuilder with a non-public type to avoid a potential future behavioral breaking change.
    private sealed class IdentityEndpointsConventionBuilder(RouteGroupBuilder inner) : IEndpointConventionBuilder
    {
        private IEndpointConventionBuilder InnerAsConventionBuilder => inner;

        public void Add(Action<EndpointBuilder> convention) => InnerAsConventionBuilder.Add(convention);
        public void Finally(Action<EndpointBuilder> finallyConvention) => InnerAsConventionBuilder.Finally(finallyConvention);
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    private sealed class FromServicesAttribute : Attribute, IFromServiceMetadata
    {
    }
}
