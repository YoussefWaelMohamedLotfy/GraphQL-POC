using GraphQL_POC.Data;
using GraphQL_POC.Schema.Filters;
using GraphQL_POC.Schema.Sorters;
using GraphQL_POC.Services.Courses;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_POC.Schema.Queries;

public class Query
{
    [UseDbContext(typeof(SchoolDbContext))]
    public async Task<IEnumerable<ISearchResultType>> Search(string term, [ScopedService] SchoolDbContext context)
    {
        var courses = await context.Courses
            .Where(c => c.Name.Contains(term))
            .Select(c => new CourseType
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId
            }).ToListAsync();

        var instructors = await context.Instructors
            .Where(i => i.FirstName.Contains(term) || i.LastName.Contains(term))
            .Select(c => new InstructorType
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Salary = c.Salary
            }).ToListAsync();

        return new List<ISearchResultType>().Concat(courses).Concat(instructors);
    }

    [GraphQLDeprecated("It got old.")]
    public string Instructions => "Hello from .NET";
}
