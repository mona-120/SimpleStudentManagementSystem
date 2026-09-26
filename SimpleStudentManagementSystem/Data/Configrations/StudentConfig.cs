using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Data.Configrations
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudentId);        // Define primary key
            builder.Property(s => s.FullName)        // make name not null and define a specific length
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(s => s.Email)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasIndex(s => s.Email)
                .IsUnique();
            builder.Property(s => s.EnrollmentDate).HasDefaultValueSql("Cast(GetDate() AS date)");

        }
    }
}
