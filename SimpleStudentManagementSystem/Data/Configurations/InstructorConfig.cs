using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Data.Configrations
{
    public class InstructorConfig : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(i => i.InstructorId);
            builder.Property(i => i.FullName)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasData(SeedData());
        }

        private static List<Instructor> SeedData()
        {
            return new List<Instructor>()
            {
                new Instructor{InstructorId = 1,FullName = "Ahmed Hassan"},
                new Instructor{InstructorId = 2,FullName = "Sarah Jenkins"},
                new Instructor{InstructorId = 3,FullName = "Mahmoud Tarek"},
                new Instructor{InstructorId = 4,FullName = "Elena Rostova"},
                new Instructor{InstructorId = 5,FullName = "Omar Farouk"}
            };
        }
    }
}
