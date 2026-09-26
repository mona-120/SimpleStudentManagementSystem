using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Data.Configrations
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.CourseId);
            builder.Property(c => c.Title)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(c => c.Description)   // description is not null to define course purpose
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
