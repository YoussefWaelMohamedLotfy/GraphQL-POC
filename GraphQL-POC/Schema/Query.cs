using Bogus;

namespace GraphQL_POC.Schema;

public class Query
{
    private readonly Faker<InstructorType> instructorFaker;

    private readonly Faker<StudentType> studentFaker;

    private readonly Faker<CourseType> courseFaker;

    public Query()
    {
        instructorFaker = new Faker<InstructorType>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Salary, f => f.Random.Double(1, 1000000));

        studentFaker = new Faker<StudentType>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.GPA, f => f.Random.Double(1, 4));

        courseFaker = new Faker<CourseType>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Name, f => f.Name.JobTitle())
            .RuleFor(c => c.Subject, f => f.PickRandom<Subject>())
            .RuleFor(c => c.Instructor, _ => instructorFaker.Generate())
            .RuleFor(c => c.Students, _ => studentFaker.Generate(3));
    }

    public IEnumerable<CourseType> GetCourses()
    {
        return courseFaker.Generate(5);
    }

    public async Task<CourseType> GetCourseByIdAsync(Guid id)
    {
        await Task.Delay(500);

        var course = courseFaker.Generate();
        course.Id = id;
        return course;
    }


    [GraphQLDeprecated("It got old.")]
    public string Instructions => "Hello from .NET";
}
