using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimpleStudentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class RestEntitiesAndRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "Cast (GetDate() As date)"),
                    Grade = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Enrollments_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstructorId);
                });

            migrationBuilder.CreateTable(
                name: "CourseInstructor",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseInstructor", x => new { x.InstructorId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_CourseInstructor_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseInstructor_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "InstructorId", "FullName" },
                values: new object[,]
                {
                    { 1, "Ahmed Hassan" },
                    { 2, "Sarah Jenkins" },
                    { 3, "Mahmoud Tarek" },
                    { 4, "Elena Rostova" },
                    { 5, "Omar Farouk" }
                });

            migrationBuilder.InsertData(
                table: "courses",
                columns: new[] { "CourseId", "Credits", "Description", "Title" },
                values: new object[,]
                {
                    { 1, 40, "Fundamentals of C# and Object-Oriented Programming (OOP).", "C#" },
                    { 2, 15, "Data querying, filtering, and manipulation techniques.", "Linq" },
                    { 3, 25, "Database management, migrations, and relationship mapping.", "EFCore" },
                    { 4, 30, "Building structured web applications using Model-View-Controller.", "MVC" },
                    { 5, 35, "Building secure RESTful web services with ASP.NET Core.", "API" }
                });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "StudentId", "DateOfBirth", "Email", "FullName" },
                values: new object[,]
                {
                    { 1, null, "youssef.karim@example.com", "Youssef Karim" },
                    { 2, new DateOnly(2003, 3, 15), "mariam.adel@example.com", "Mariam Adel" },
                    { 3, new DateOnly(2005, 7, 20), "liam.carter@example.com", "Liam Carter" },
                    { 4, new DateOnly(2006, 1, 12), "nour.eldin@example.com", "Nour El-Din" },
                    { 5, null, "sofia.martinez@example.com", "Sofia Martinez" }
                });

            migrationBuilder.InsertData(
                table: "CourseInstructor",
                columns: new[] { "CourseId", "InstructorId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 5, 3 },
                    { 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "CourseId", "StudentId", "Grade" },
                values: new object[,]
                {
                    { 1, 1, 90m },
                    { 4, 1, 80m },
                    { 1, 2, 85m },
                    { 2, 3, null },
                    { 3, 5, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseInstructor_CourseId",
                table: "CourseInstructor",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseInstructor");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "CourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "CourseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "CourseId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "CourseId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "CourseId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "StudentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "StudentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "StudentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "StudentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "StudentId",
                keyValue: 5);
        }
    }
}
