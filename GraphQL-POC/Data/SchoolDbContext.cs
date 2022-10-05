using GraphQL_POC.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_POC.Data;

public sealed class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<CourseDTO> Courses { get; set; }
    public DbSet<InstructorDTO> Instructors { get; set; }
    public DbSet<StudentDTO> Students { get; set; }
}
