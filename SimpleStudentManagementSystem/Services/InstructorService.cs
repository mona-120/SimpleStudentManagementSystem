using Microsoft.EntityFrameworkCore;
using SimpleStudentManagementSystem.Common;
using SimpleStudentManagementSystem.Data;
using SimpleStudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Services
{
    internal class InstructorService
    {
        private readonly AppDbContext _context;
        public InstructorService(AppDbContext context)
        {
            _context = context;
        }


        // Add instructor
        public async Task<Result<Instructor>> AddInstructor(Instructor instructor)
        {
            if (string.IsNullOrWhiteSpace(instructor.FullName))
                return new Result<Instructor>(false, "Invalid input, please enter instructor name", instructor);

            if(instructor.FullName.Length > 50)
                return new Result<Instructor>(false, "Invalid input, Instructor name must be <= 50", instructor);

            await _context.AddAsync(instructor);
            await _context.SaveChangesAsync();
            return new Result<Instructor>(true, $"Instructor {instructor.FullName} Added Successfully", instructor);
        }


        // Get instructor by id
        public async Task<Result<Instructor>> GetInstructorById(int id)
        {
            Instructor? instructor = await _context.Instructors.FindAsync(id);

            if(instructor == null)
                return new Result<Instructor>(false, "Invalid input, Instructor Doesn't Exist", instructor);
            return new Result<Instructor>(true, $"Instructor {instructor.FullName} Found", instructor);
        }


        // update instructor
        public async Task<Result<Instructor>> UpdateInstructor(int id,string UpdatedName)
        {
            Instructor? instructor = await _context.Instructors.FindAsync(id);

            if (instructor == null)
                return new Result<Instructor>(false, "Invalid input, Instructor Doesn't Exist", instructor);

            if (string.IsNullOrWhiteSpace(UpdatedName))
                return new Result<Instructor>(false, "Invalid input, please enter instructor name", instructor);

            if (UpdatedName.Length > 50)
                return new Result<Instructor>(false, "Invalid input, Instructor name must be <= 50", instructor);

            instructor.FullName = UpdatedName;
            await _context.SaveChangesAsync();

            return new Result<Instructor>(true, $"Instructor with id {id} name updated to be {UpdatedName}", instructor);
        }


        // Delete instructor
        public async Task<Result<Instructor>> DeleteInstructor(int id)
        {
            Instructor? instructor = await _context.Instructors.FindAsync(id);

            if (instructor == null)
                return new Result<Instructor>(false, "Invalid input, Instructor Doesn't Exist", instructor);

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return new Result<Instructor>(true, $"Instructor {instructor.FullName} Deleted Successfully", instructor);
        }


        // Get All Instructors
        public async Task<Result<List<Instructor>>> GetInstructors()
        {
            List<Instructor> instructors = await _context.Instructors.AsNoTracking().ToListAsync();
            if (instructors.Count == 0)
                return new Result<List<Instructor>>(false, "Instructor List is Empty", []);
            return new Result<List<Instructor>>(true, "Instructor List Retrived successfully", instructors);
        }

    }
}
