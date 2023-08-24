using Application.Interfaces;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Shared;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace CleanCodeArchitecture.Services
{
    public static class AddBaseServices
    {
        public static IServiceCollection AddAppBaseServices(this IServiceCollection _services)
        {
            _services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            _services.AddHttpContextAccessor();
            _services.AddTransient<ICurrentUserService, CurrentUserService>();

            _services.AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearer =>
            {
                var key = Encoding.ASCII.GetBytes("xWmFhO5woZO7GWoIuBcX6XuNuInGncYqFsC4QrgRs");

                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero
                };

                bearer.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c =>
                    {
                        if (c.Exception is SecurityTokenExpiredException)
                        {
                            c.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            c.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(c.Exception.Message);
                            return c.Response.WriteAsync(result);
                        }
                        else
                        {
                            c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            c.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(c.Exception.Message);
                            return c.Response.WriteAsync(result);
                        }
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        if (!context.Response.HasStarted)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject("");
                            return context.Response.WriteAsync(result);
                        }

                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject("");
                        return context.Response.WriteAsync(result);
                    }
                };
            });

            var type = typeof(PermissionsConstants);
            var campos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            var constantes = campos.Where(campo => campo.IsLiteral && campo.FieldType == typeof(string));

            var valoresConstantes = new List<string>();
            foreach (var campo in constantes)
            {
                var valor = campo.GetValue(null) as string;
                valoresConstantes.Add(valor);
            }

            _services.AddAuthorization(options =>
            {
                foreach (var v in valoresConstantes)
                    options.AddPolicy(v, policy => policy.RequireClaim(ClaimTypes.AuthenticationMethod, v));
            });

            return _services;
        }
    }
}
