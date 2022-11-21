using GraphQL.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StrawberryShake;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services
        .AddGraphQLClient()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7037/graphql"));

        services.AddHostedService<Startup>();
    })
    .Build()
    .Run();

public class Startup : IHostedService
{

    private readonly IGraphQLClient _client;

    public Startup(IGraphQLClient client)
    {
        _client = client;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        IOperationResult<IGetInstructionsResult> result = await _client.GetInstructions.ExecuteAsync();
        IOperationResult<IGetCoursesResult> coursesResult = await _client.GetCourses.ExecuteAsync();

        if (result.IsErrorResult() || coursesResult.IsErrorResult())
        {
            Console.WriteLine("Failed to get courses");
        }
        else
        {
            Console.WriteLine(result.Data?.Instructions);

            foreach (var c in coursesResult.Data?.Courses)
            {
                Console.WriteLine($"{c.Name} is taught by {c.Instructor.FirstName}");
            }
        }

        var courseByIdResult = await _client.GetCourseById.ExecuteAsync(Guid.Parse("24A53179-5C8B-493F-B85C-EF7B34315FBD"));

        if (courseByIdResult.IsErrorResult())
        {
            Console.WriteLine("Failed to get course");
        }
        else
        {
            var course = courseByIdResult.Data?.CourseById;
            Console.WriteLine($"Course {course.Id} name is {course.Name}");
        }

        var createCourseResult = await _client.CreateCourse.ExecuteAsync(new()
        {
            Name = "GraphQL 101",
            Subject = Subject.Science,
            InstructorId = Guid.NewGuid()
        });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
    
