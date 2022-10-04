using GraphQL_POC.Schema.Subscriptions;
using HotChocolate.Subscriptions;

namespace GraphQL_POC.Schema.Mutations;

public class Mutation
{
    private readonly List<CourseResult> _courses;

    public Mutation()
    {
        _courses = new();
    }

    public async Task<CourseResult> CreateCourse(CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
    {
        CourseResult course = new()
        {
            Id = Guid.NewGuid(),
            Name = courseInput.Name,
            Subject = courseInput.Subject,
            InstructorId = courseInput.InstructorId
        };

        _courses.Add(course);
        await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);
        return course;
    }

    public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
    {
        CourseResult course = _courses.FirstOrDefault(c => c.Id == id);

        if (course is null)
            throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));

        course.Name = courseInput.Name;
        course.Subject = courseInput.Subject;
        course.InstructorId = courseInput.InstructorId;

        string updatedCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
        await topicEventSender.SendAsync(updatedCourseTopic, course);

        return course;
    }

    public bool DeleteCourse(Guid id)
    {
        return _courses.RemoveAll(c => c.Id == id) >= 1;
    }
}
