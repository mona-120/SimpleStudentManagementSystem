using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Data.Configrations
{
    public class CourseInstructorConfig : IEntityTypeConfiguration<CourseInstructor>
    {
        public void Configure(EntityTypeBuilder<CourseInstructor> builder)
        {
            builder.HasKey(ci => new {ci.InstructorId, ci.CourseId});

            builder.HasOne(ci => ci.instructor)
                .WithMany(i => i.courses)
                .HasForeignKey(ci => ci.InstructorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);  // if delete instructor, we should delete record in CourseInstructor

            builder.HasOne(ci => ci.course)
                .WithMany(c => c.instructors)
                .HasForeignKey(ci => ci.CourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(SeedData());
        }

        private static List<CourseInstructor> SeedData()
        {
            return new List<CourseInstructor>
            {
                new CourseInstructor { InstructorId = 1, CourseId = 1},
                new CourseInstructor { InstructorId = 2, CourseId = 1},
                new CourseInstructor { InstructorId = 2, CourseId = 2},
                new CourseInstructor { InstructorId = 3, CourseId = 5},
                new CourseInstructor { InstructorId = 4, CourseId = 3},
                // course 4 can be exist with out an instructor ,and instructor can be exist with out teaching a course
                // Relation is many-to-many and partial between instructor and course but required in CourseInstructor
            };
        }
    }
}
