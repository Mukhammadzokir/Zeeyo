using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zeeyo.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedEntitiesMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Courses_CourseId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Branches_BranchId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Courses_CourseId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Courses_CourseId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "TeachersCourse");

            migrationBuilder.DropIndex(
                name: "IX_Users_CourseId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Courses_BranchId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Lessons",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons",
                newName: "IX_Lessons_GroupId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Enrollments",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                newName: "IX_Enrollments_GroupId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Attendances",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_CourseId",
                table: "Attendances",
                newName: "IX_Attendances_GroupId");

            migrationBuilder.CreateTable(
                name: "BranchCourses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchCourses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeacherId = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherGroups_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchCourses_BranchId",
                table: "BranchCourses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchCourses_CourseId",
                table: "BranchCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CourseId",
                table: "Groups",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGroups_GroupId",
                table: "TeacherGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGroups_TeacherId",
                table: "TeacherGroups",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Groups_GroupId",
                table: "Attendances",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Groups_GroupId",
                table: "Enrollments",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Groups_GroupId",
                table: "Lessons",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Groups_GroupId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Groups_GroupId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Groups_GroupId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "BranchCourses");

            migrationBuilder.DropTable(
                name: "TeacherGroups");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Lessons",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_GroupId",
                table: "Lessons",
                newName: "IX_Lessons_CourseId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Enrollments",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_GroupId",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Attendances",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_GroupId",
                table: "Attendances",
                newName: "IX_Attendances_CourseId");

            migrationBuilder.AddColumn<long>(
                name: "CourseId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "Courses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Courses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Courses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndTime",
                table: "Courses",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartTime",
                table: "Courses",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.CreateTable(
                name: "TeachersCourse",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    TeacherId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeachersCourse_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachersCourse_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CourseId",
                table: "Users",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_BranchId",
                table: "Courses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersCourse_CourseId",
                table: "TeachersCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersCourse_TeacherId",
                table: "TeachersCourse",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Courses_CourseId",
                table: "Attendances",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Branches_BranchId",
                table: "Courses",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Courses_CourseId",
                table: "Lessons",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Courses_CourseId",
                table: "Users",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
