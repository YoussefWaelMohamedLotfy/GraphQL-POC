using GraphQL_POC.Models;

namespace GraphQL_POC.Schema.Mutations;

public class CourseResult
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Subject Subject { get; set; }

    public Guid InstructorId { get; set; }
}
