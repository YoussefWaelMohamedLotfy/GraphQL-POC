using GraphQL_POC.Services.Courses;

namespace GraphQL_POC.Schema.Queries;

public class Query
{
    private readonly CourseRepository _courseRepository;

    public Query(CourseRepository courseRepository)
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


    [GraphQLDeprecated("It got old.")]
    public string Instructions => "Hello from .NET";
}
