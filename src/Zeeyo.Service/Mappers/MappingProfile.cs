using AutoMapper;
using Zeeyo.Service.DTOs.Logins;
using Zeeyo.Service.DTOs.Branches;
using Zeeyo.Domain.Entities.Users;
using Zeeyo.Service.DTOs.Payments;
using Zeeyo.Domain.Entities.Roles;
using Zeeyo.Domain.Entities.Branches;
using Zeeyo.Domain.Entities.Courses;
using Zeeyo.Domain.Entities.Payments;
using Zeeyo.Service.DTOs.Roles.Roles;
using Zeeyo.Domain.Entities.Students;
using Zeeyo.Domain.Entities.Teachers;
using Zeeyo.Service.DTOs.Users.Users;
using Zeeyo.Service.DTOs.Courses.Courses;
using Zeeyo.Service.DTOs.Courses.Lessons;
using Zeeyo.Service.DTOs.Users.UserCodes;
using Zeeyo.Service.DTOs.Users.UserRoles;
using Zeeyo.Service.DTOs.Roles.Permissions;
using Zeeyo.Service.DTOs.Students.Students;
using Zeeyo.Service.DTOs.Teachers.Teachers;
using EduNet.Backend.Service.DTOs.Roles.Roles;
using Zeeyo.Service.DTOs.Students.Enrollments;
using Zeeyo.Service.DTOs.Students.Attendances;
using Zeeyo.Service.DTOs.Roles.RolePermissions;
using Zeeyo.Service.DTOs.Teachers.TeacherCourses;

namespace Zeeyo.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<User, UserForResultDto>();
        CreateMap<User, UserForUpdateDto>();
        CreateMap<User, UserForCreationDto>();
        CreateMap<UserProfilePhoto, UserProfilePhotoForResultDto>();

        // Role
        CreateMap<Role, RoleForResultDto>();
        CreateMap<Role, RoleForUpdateDto>();
        CreateMap<Role, RoleForCreationDto>();

        // Course
        CreateMap<Course, CourseForResultDto>().ReverseMap();
        CreateMap<Course, CourseForUpdateDto>().ReverseMap();
        CreateMap<Course, CourseForCreationDto>().ReverseMap();

        // Branch
        CreateMap<Branch, BranchForResultDto>().ReverseMap();
        CreateMap<Branch, BranchForUpdateDto>().ReverseMap();
        CreateMap<Branch, BranchForCreationDto>().ReverseMap();

        // Lesson
        CreateMap<Lesson, LessonForResultDto>().ReverseMap();
        CreateMap<Lesson, LessonForUpdateDto>().ReverseMap();
        CreateMap<Lesson, LessonForCreationDto>().ReverseMap();

        // Teacher
        CreateMap<User, TeacherForResultDto>().ReverseMap();
        CreateMap<User, TeacherForUpdateDto>().ReverseMap();
        CreateMap<User, TeacherForCreationDto>().ReverseMap();
        CreateMap<UserProfilePhoto, TeacherProfilePhotoForResultDto>().ReverseMap();

        // Payment
        CreateMap<Payment, PaymentForResultDto>().ReverseMap();
        CreateMap<Payment, PaymentForUpdateDto>().ReverseMap();
        CreateMap<Payment, PaymentForCreationDto>().ReverseMap();

        // Student
        CreateMap<User, StudentForResultDto>().ReverseMap();
        CreateMap<User, StudentForUpdateDto>().ReverseMap();
        CreateMap<User, StudentForCreationDto>().ReverseMap();
        CreateMap<UserProfilePhoto, StudentProfilePhotoForResultDto>().ReverseMap();

        // UserRole
        CreateMap<UserRole, UserRoleForResultDto>().ReverseMap();
        CreateMap<UserRole, UserRoleForUpdateDto>().ReverseMap();
        CreateMap<UserRole, UserRoleForCreationDto>().ReverseMap();

        // UserCode
        CreateMap<UserCode, UserCodeForResultDto>().ReverseMap();
        CreateMap<UserCode, UserCodeForCreationDto>().ReverseMap();

        // Attendance
        CreateMap<Attendance, AttendanceForResultDto>().ReverseMap();
        CreateMap<Attendance, AttendanceForUpdateDto>().ReverseMap();
        CreateMap<Attendance, AttendanceForCreationDto>().ReverseMap();

        // Permission
        CreateMap<Permission, PermissionForResultDto>().ReverseMap();
        CreateMap<Permission, PermissionForUpdateDto>().ReverseMap();
        CreateMap<Permission, PermissionForCreationDto>().ReverseMap();

        // Enrollment
        CreateMap<Enrollment, EnrollmentForResultDto>().ReverseMap();
        CreateMap<Enrollment, EnrollmentForUpdateDto>().ReverseMap();
        CreateMap<Enrollment, EnrollmentForCreationDto>().ReverseMap();

        // Login
        CreateMap<LoginForCreationDto, LoginForResultDto>().ReverseMap();

        // TeacherCourse
        CreateMap<TeacherCourse, TeacherCourseForResultDto>().ReverseMap();
        CreateMap<TeacherCourse, TeacherCourseForUpdateDto>().ReverseMap();
        CreateMap<TeacherCourse, TeacherCourseForCreationDto>().ReverseMap();

        // RolePermission
        CreateMap<RolePermission, RolePermissionForResultDto>().ReverseMap();
        CreateMap<RolePermission, RolePermissionForUpdateDto>().ReverseMap();
        CreateMap<RolePermission, RolePermissionForCreationDto>().ReverseMap();
    }
}
