using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure;
using Api.Service;
using Api.Core;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;
        var services = builder.Services;
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        
        var keyString = configuration.GetValue<string>("JWT_SECRET_KEY") ?? throw new ArgumentNullException("JWT_SECRET_KEY is required");
        var issuer = configuration.GetValue<string>("JWT_ISSUER");
        var audience = configuration.GetValue<string>("JWT_AUDIENCE");

        builder.Services.AddSwaggerGen(c =>
        {
            c.AddServer(new OpenApiServer { Url = $"/api" });
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Template Api", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Por favor ingrese el token JWT con el prefijo 'Bearer '",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString)),
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidIssuer = issuer,
                ValidAudience = audience,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero
            };
            options.UseSecurityTokenValidators = true;
            options.IncludeErrorDetails = true;
        });

        // builder.Services.AddCors(options =>
        // {
        //     options.AddPolicy("AllowTemplate", policy =>
        //     {
        //         policy.WithOrigins("*")
        //               .AllowAnyHeader()
        //               .AllowAnyMethod();
        //     });
        // });

        InfrastructureModule.ConfigureServices(services, configuration);

        services.AddScoped<JwtService>();
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<ILoginService, LoginService>();

        builder.Services.AddHealthChecks();

        var app = builder.Build();

        app.UsePathBase("/api");


        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/api/swagger/v1/swagger.json", "Money Control Api");
        });


        app.UseRouting();

        // app.UseCors("AllowTemplate");
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.UseHealthChecks("/health");

        app.Run();
    }
}
