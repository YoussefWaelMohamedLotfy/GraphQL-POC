using GraphQL_POC.Data;
using GraphQL_POC.Schema.Filters;
using GraphQL_POC.Schema.Sorters;
using GraphQL_POC.Services.Courses;

namespace GraphQL_POC.Schema.Queries;

[ExtendObjectType(typeof(Query))]
public class CourseQuery
{
    private readonly CourseRepository _courseRepository;

    public CourseQuery(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<CourseType>> GetCourses()
    {
        var courseDTOs = await _courseRepository.GetAll();
        return courseDTOs.Select(c => new CourseType
        {
            Id = c.Id,
            Name = c.Name,
            Subject = c.Subject,
            InstructorId = c.InstructorId
        });
    }

    [UseDbContext(typeof(SchoolDbContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection]
    [UseFiltering(typeof(CourseFilterType))]
    [UseSorting(typeof(CourseSortType))]
    public IQueryable<CourseType> GetPaginatedCourses([ScopedService] SchoolDbContext context)
    {
        return context.Courses.Select(c => new CourseType
        {
            Id = c.Id,
            Name = c.Name,
            Subject = c.Subject,
            InstructorId = c.InstructorId
        });
    }

    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    public async Task<IEnumerable<CourseType>> GetOffsetCourses()
    {
        var courseDTOs = await _courseRepository.GetAll();
        return courseDTOs.Select(c => new CourseType
        {
            Id = c.Id,
            Name = c.Name,
            Subject = c.Subject,
            InstructorId = c.InstructorId
        });
    }

    public async Task<CourseType> GetCourseByIdAsync(Guid id)
    {
        var courseDTO = await _courseRepository.GetById(id);
        return new CourseType
        {
            Id = courseDTO.Id,
            Name = courseDTO.Name,
            Subject = courseDTO.Subject,
            InstructorId = courseDTO.InstructorId
        };
    }
}
