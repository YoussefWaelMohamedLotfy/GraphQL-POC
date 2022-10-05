using GraphQL_POC.Data;
using GraphQL_POC.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_POC.Services.Instructors;

public class InstructorRepository
{
    private readonly SchoolDbContext _context;

    public InstructorRepository(IDbContextFactory<SchoolDbContext> contextFactory)
    {
        _context = contextFactory.CreateDbContext();
    }

    public async Task<InstructorDTO> GetById(Guid id)
    {
        return await _context.Instructors
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<InstructorDTO>> GetManyByIds(IReadOnlyList<Guid> instructorIds)
    {
        return await _context.Instructors
            .Where(c => instructorIds.Contains(c.Id))
            .ToListAsync();
    }
}
