using AutoMapper;
using Zeeyo.Service.DTOs.Logins;
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
using Zeeyo.Service.DTOs.Courses.Groups;
using Zeeyo.Service.DTOs.Courses.Courses;
using Zeeyo.Service.DTOs.Courses.Lessons;
using Zeeyo.Service.DTOs.Users.UserCodes;
using Zeeyo.Service.DTOs.Users.UserRoles;
using Zeeyo.Service.DTOs.Branches.Branches;
using Zeeyo.Service.DTOs.Roles.Permissions;
using Zeeyo.Service.DTOs.Students.Students;
using Zeeyo.Service.DTOs.Teachers.Teachers;
using EduNet.Backend.Service.DTOs.Roles.Roles;
using Zeeyo.Service.DTOs.Students.Enrollments;
using Zeeyo.Service.DTOs.Students.Attendances;
using Zeeyo.Service.DTOs.Roles.RolePermissions;
using Zeeyo.Service.DTOs.Branches.BranchCourses;
using Zeeyo.Service.DTOs.Teachers.TeacherGroups;
using Zeeyo.Domain.Entities.Contacts;
using Zeeyo.Service.DTOs.Contacts;

namespace Zeeyo.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        // Role
        CreateMap<Role, RoleForResultDto>().ReverseMap();
        CreateMap<Role, RoleForUpdateDto>().ReverseMap();
        CreateMap<Role, RoleForCreationDto>().ReverseMap();

        CreateMap<Contact, ContactDto>().ReverseMap();

        // User
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<UserProfilePhoto, UserProfilePhotoForResultDto>().ReverseMap();

        // Group
        CreateMap<Group, GroupForResultDto>().ReverseMap();
        CreateMap<Group, GroupForUpdateDto>().ReverseMap();
        CreateMap<Group, GroupForCreationDto>().ReverseMap();


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

        // BranchCourse
        CreateMap<BranchCourse, BranchCourseForResultDto>().ReverseMap();
        CreateMap<BranchCourse, BranchCourseForUpdateDto>().ReverseMap();
        CreateMap<BranchCourse, BranchCourseForCreationDto>().ReverseMap();

        // Login
        CreateMap<LoginForCreationDto, LoginForResultDto>().ReverseMap();

        // TeacherCourse
        CreateMap<TeacherGroup, TeacherGroupForResultDto>().ReverseMap();
        CreateMap<TeacherGroup, TeacherGroupForUpdateDto>().ReverseMap();
        CreateMap<TeacherGroup, TeacherGroupForCreationDto>().ReverseMap();

        // RolePermission
        CreateMap<RolePermission, RolePermissionForResultDto>().ReverseMap();
        CreateMap<RolePermission, RolePermissionForUpdateDto>().ReverseMap();
        CreateMap<RolePermission, RolePermissionForCreationDto>().ReverseMap();
    }
}
