using GraphQL_POC.DataLoaders;
using GraphQL_POC.Models;

namespace GraphQL_POC.Schema.Queries;

public class CourseType
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Subject Subject { get; set; }

    public Guid InstructorId { get; set; }

    [GraphQLNonNullType]
    public async Task<InstructorType> Instructor([Service] InstructorDataLoader dataLoader)
    {
        var instructorDto = await dataLoader.LoadAsync(InstructorId, CancellationToken.None);

        return new InstructorType()
        {
            Id = instructorDto.Id,
            FirstName = instructorDto.FirstName,
            LastName = instructorDto.LastName,
            Salary = instructorDto.Salary
        };
    }

    public IEnumerable<StudentType> Students { get; set; }
}
