using GraphQL_POC.Data;
using GraphQL_POC.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_POC.Services.Courses;

public sealed class CourseRepository
{
    private readonly SchoolDbContext _context;

    public CourseRepository(IDbContextFactory<SchoolDbContext> contextFactory)
    {
        _context = contextFactory.CreateDbContext();
    }

    public async Task<IEnumerable<CourseDTO>> GetAll()
    {
        return await _context.Courses
            .ToListAsync();
    }

    public async Task<CourseDTO> GetById(Guid id)
    {
        return await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<CourseDTO> Create(CourseDTO course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return course;
    }

    public async Task<CourseDTO> Update(CourseDTO course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();

        return course;
    }

    public async Task<bool> Delete(Guid id)
    {
        _context.Courses.Remove(new() { Id = id });
        return await _context.SaveChangesAsync() >= 1;
    }
}
