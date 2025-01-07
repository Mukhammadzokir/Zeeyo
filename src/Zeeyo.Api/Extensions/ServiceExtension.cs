using System.Text;
using System.Reflection;
using Zeeyo.Service.Mappers;
using Zeeyo.Data.Repositories;
using Microsoft.OpenApi.Models;
using Zeeyo.Data.IRepositories;
using Microsoft.IdentityModel.Tokens;
using Zeeyo.Service.Services.Branches;
using Zeeyo.Service.Interfaces.Branches;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Zeeyo.Service.Interfaces.Users;
using Zeeyo.Service.Services.Users;
using Zeeyo.Service.Interfaces.Students;
using Zeeyo.Service.Services.Students;
using Zeeyo.Service.Interfaces.Teachers;
using Zeeyo.Service.Services.Teachers;

namespace Zeeyo.Api.Extensions;

public static class ServiceExtension
{
    public static void AddCustomService(this IServiceCollection services)
    {
        // Mapper
        services.AddAutoMapper(typeof(MappingProfile));
        
        // Repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherService, TeacherService>();
    }

    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var Key = Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.FromMinutes(1)
            };
        });
    }

    public static void AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Zeeyo.Api", Version = "v1" });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
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
                    new string[] { }
                }
            });
        });
    }
}
