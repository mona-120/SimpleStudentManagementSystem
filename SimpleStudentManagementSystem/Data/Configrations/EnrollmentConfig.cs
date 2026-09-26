using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Data.Configrations
{
    public class EnrollmentConfig : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => new { e.StudentId, e.CourseId}); // composite key
            builder.Property(e => e.EnrollmentDate)
                .HasDefaultValueSql("Cast (GetDate() As date)");

            builder.HasOne(e => e.course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .IsRequired();   // total participation from enrollment where dependent(CourseID) can't exist without parent(course)
            
            builder.HasOne(e => e.student)
                 .WithMany(s => s.Enrollments)
                 .HasForeignKey(e => e.StudentId)
                 .IsRequired();

            // Default dalate is on delete cascade as participation is required

            builder.HasData(SeedData());
        }

        private static List<Enrollment> SeedData()
        {
            return new List<Enrollment>
            {
                new Enrollment{StudentId = 1 , CourseId = 1,Grade = 90},
                new Enrollment{StudentId = 2 , CourseId = 1,Grade = 85},
                new Enrollment{StudentId = 3 , CourseId = 2,Grade = null},
                new Enrollment{StudentId = 1 , CourseId = 4,Grade = 80},
                new Enrollment{StudentId = 5 , CourseId = 3,Grade = null},
            };
        }
    }
}
