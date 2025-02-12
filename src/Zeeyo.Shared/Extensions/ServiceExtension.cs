using Zeeyo.Data.Repositories;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.Services.Auth;
using Zeeyo.Service.Services.Users;
using Zeeyo.Service.Services.Roles;
using Zeeyo.Service.Interfaces.Auth;
using Microsoft.IdentityModel.Tokens;
using Zeeyo.Service.Interfaces.Roles;
using Zeeyo.Service.Interfaces.Users;
using Zeeyo.Service.Services.Courses;
using Zeeyo.Service.Services.Accounts;
using Zeeyo.Service.Services.Branches;
using Zeeyo.Service.Services.Payments;
using Zeeyo.Service.Services.Students;
using Zeeyo.Service.Services.Teachers;
using Zeeyo.Service.Interfaces.Courses;
using Zeeyo.Service.Interfaces.Teachers;
using Zeeyo.Service.Interfaces.Accounts;
using Zeeyo.Service.Interfaces.Branches;
using Zeeyo.Service.Interfaces.Payments;
using Zeeyo.Service.Interfaces.Students;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Zeeyo.Service.Interfaces.Contacts;
using Zeeyo.Service.Services.Contacts;


namespace Zeeyo.Shared.Extensions;

public static class ServiceExtension
{
    public static void AddCustomService(this IServiceCollection services)
    {
        // Repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IContactRepository, ContactRepository>();

        // Services
        services.AddScoped<ISmsService, SmsService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IContactService, ContactSerivce>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();
        services.AddScoped<IAttendanceService, AttendanceService>();
        services.AddScoped<ITeacherGroupService, TeacherGroupService>();
        services.AddScoped<IBranchCourseService, BranchCourseService>();
        services.AddScoped<IRolePermissionService, RolePermissionService>();
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

            o.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.Request.Cookies["AuthToken"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        context.Token = token;
                    }
                    return Task.CompletedTask;
                }
            };

            // ✅ Register Authorization
            services.AddAuthorization();
        });
    }
}