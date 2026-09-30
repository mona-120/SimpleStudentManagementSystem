using SimpleStudentManagementSystem.Data;
using SimpleStudentManagementSystem.Entities;
using SimpleStudentManagementSystem.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Services
{
    public class StudentService
    {
        private readonly AppDbContext context;
        public StudentService(AppDbContext context_)
        {
            context = context_;
        }


        // Add a student and check if configrations is true
        public async Task<Result<Student>> AddStudent(Student student)
        {
            if(string.IsNullOrWhiteSpace(student.FullName))
                return new Result<Student>(false, "Invalid input ,please enter your name!",student);

            if(student.FullName.Length > 50)
                return new Result<Student>(false, "Invalid input ,name must be <= 50", student);

            if (string.IsNullOrWhiteSpace(student.Email))
                return new Result<Student>(false, "Invalid input ,please enter your Email!", student);

            if (student.Email.Length > 100)
                return new Result<Student>(false, "Invalid input ,Email must be <= 100", student);

            if (await context.Students.AnyAsync(s => s.Email == student.Email))
                return new Result<Student>(false, "Invalid input ,Your Email Already exist!", student);

            if (student.DateOfBirth != null && student.DateOfBirth > DateOnly.FromDateTime(DateTime.Now))
                return new Result<Student>(false, "Invalid input ,Your DateOfBirth can't be in the future!", student);


            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
            return new Result<Student>(true, $"Added Student {student.FullName} Successfully", student);
        }


        // Get Student by id
        public async Task<Result<Student>> GetStudentByID(int id)
        {
            Student? student = await context.Students.AsNoTracking().FirstOrDefaultAsync(s=> s.StudentId == id);
            if (student == null)
            {
                return new Result<Student>(false, $"System doesn't contain a student with id {id}",student);
            }
            return new Result<Student>(true, $"Find student {student.FullName} with id {id}", student);
        }

        

        // update student data
        public async Task<Result<Student>> UpdateStudent(int id, Student student)
        {
            Student? st = await context.Students.FindAsync(id);
            if (st == null)
            {
                return new Result<Student>(false, $"System doesn't contain a student with id {id}", st);
            }

            if (st.FullName != student.FullName)      // Edit condition to check only the changed property
            {
                if (string.IsNullOrWhiteSpace(student.FullName))
                    return new Result<Student>(false, "Invalid input ,please enter your name!", student);

                if (student.FullName.Length > 50)
                    return new Result<Student>(false, "Invalid input ,name must be <= 50", student);

                st.FullName = student.FullName;
            }

            if (st.Email != student.Email)
            {
                if (string.IsNullOrWhiteSpace(student.Email))
                    return new Result<Student>(false, "Invalid input ,please enter your Email!", student);

                if (student.Email.Length > 100)
                    return new Result<Student>(false, "Invalid input ,Email must be <= 100", student);

                if (await context.Students.AnyAsync(s => s.Email == student.Email))
                    return new Result<Student>(false, "Invalid input ,Your Email Already exist!", student);

                st.Email = student.Email;
            }

            if (st.DateOfBirth != student.DateOfBirth)
            {
                if (student.DateOfBirth != null && student.DateOfBirth > DateOnly.FromDateTime(DateTime.Now))
                    return new Result<Student>(false, "Invalid input ,Your DateOfBirth can't be in the future!", student);

                st.DateOfBirth = student.DateOfBirth;
            }
            
            await context.SaveChangesAsync();
            return new Result<Student>(true, $"Update Student with id {id}, and His data became " +
                $"{st.FullName} - {st.Email} - {st.DateOfBirth}",student);
        }


        // Delete a student
        public async Task<Result<Student>> DeleteStudent(int id)
        {
            Student? student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return new Result<Student>(false, $"System doesn't contain a student with id {id}", student);
            }
            //context.Students.Remove(student);  // Hard Delete
            SoftDelete(student);  // Soft Delete
            await context.SaveChangesAsync();
            return new Result<Student>(true, $"Delete Student {student.FullName} with id {id} Successfully", student);
        }



        // Get Student by name (partial match)
        public async Task<Result<List<Student>>> GetStudentByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new Result<List<Student>>(false, "Enter the student name", [ ]);

            List<Student> student = await context.Students.Where(s => s.FullName.Contains(name)).AsNoTracking().ToListAsync();
            
            if(student.Count == 0)
                return new Result<List<Student>>(false, "student doesn't exist", []);

            return new Result<List<Student>>(true, $"Found students with matched name {name}", student);
        }



        // Get All students
        public async Task<Result<List<Student>>> GetStudentsList()
        {
            List<Student> students = await context.Students.AsNoTracking().ToListAsync();
            if(students.Count == 0)
                return new Result<List<Student>>(false, "Students List is empty", []);

            return new Result<List<Student>>(true,"Students retrieved successfully",students);  
        }



        // get students with all their enrollments
        // using Eager Loading to get all data in one query and avoid N+1 problem ,that occurs as a result of
        // Lazy Loading as it retrive all student data first then retrive enrollment for each student in a query that repeat N times
        public async Task<Result<List<Student>>> GetStudentInfo()
        {
            List<Student> students = await context.Students.Include(s => s.Enrollments).AsNoTracking().ToListAsync();

            if(students.Count == 0)
                return new Result<List<Student>>(false, "Students List is empty", []);

            return new Result<List<Student>>(true, "Students retrieved with their enrollments successfully", students);
        }



        // Soft Delete method
        private void SoftDelete(Student student)
        {
            student.IsDeleted = true;
        }

        // Get Deleted Students by ignoring filter
        public async Task<Result<List<Student>>> GetDeletedStudents()
        {
            var res = await context.Students.IgnoreQueryFilters()
                               .Where(s=> s.IsDeleted == true)
                               .AsNoTracking().ToListAsync();
            if(res.Count == 0)
                return new Result<List<Student>>(false, "No Deleted Students", []);

            return new Result<List<Student>>(true, "Deleted Students Retrieved Successfully!", res);
        }
    }
}
