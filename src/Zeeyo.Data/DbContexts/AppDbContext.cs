using Zeeyo.Domain.Entities.Roles;
using Zeeyo.Domain.Entities.Users;
using Zeeyo.Domain.Entities.Assets;
using Microsoft.EntityFrameworkCore;
using Zeeyo.Domain.Entities.Courses;
using Zeeyo.Domain.Entities.Branches;
using Zeeyo.Domain.Entities.Messages;
using Zeeyo.Domain.Entities.Payments;
using Zeeyo.Domain.Entities.Students;
using Zeeyo.Domain.Entities.Teachers;

namespace Zeeyo.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<Asset> Assets { get; set; }
    DbSet<Course> Courses { get; set; }
    DbSet<Lesson> Lessons { get; set; }
    DbSet<Branch> Branches { get; set; }
    DbSet<Payment> Payments { get; set; }
    DbSet<Teacher> Teachers { get; set; }
    DbSet<Message> Messages { get; set; }
    DbSet<Student> Students { get; set; }
    DbSet<UserCode> UserCodes { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    DbSet<Permission> Permissions { get; set; }
    DbSet<Attendance> Attendances { get; set; }
    DbSet<Enrollment> Enrollments { get; set; }
    DbSet<TeacherCourse> TeachersCourse { get; set; }
    DbSet<RolePermission> RolePermissions { get; set; }
    DbSet<TeacherProfilePhoto> TeachersProfilePhoto { get; set; }
    DbSet<StudentProfilePhoto> StudentProfilesPhoto { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}