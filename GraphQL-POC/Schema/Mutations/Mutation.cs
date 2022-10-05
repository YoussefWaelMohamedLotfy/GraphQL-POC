using AppAny.HotChocolate.FluentValidation;
using GraphQL_POC.DTOs;
using GraphQL_POC.Schema.Subscriptions;
using GraphQL_POC.Services.Courses;
using GraphQL_POC.Validators;
using HotChocolate.Subscriptions;

namespace GraphQL_POC.Schema.Mutations;

public class Mutation
{
    private readonly CourseRepository _courseRepository;

    public Mutation(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<CourseResult> CreateCourse([UseFluentValidation] CourseInputType courseInput,
                                                 [Service] ITopicEventSender topicEventSender)
    {
        CourseDTO courseDTO = new()
        {
            Name = courseInput.Name,
            Subject = courseInput.Subject,
            InstructorId = courseInput.InstructorId
        };

        courseDTO = await _courseRepository.Create(courseDTO);

        CourseResult course = new()
        {
            Id = courseDTO.Id,
            Name = courseDTO.Name,
            Subject = courseDTO.Subject,
            InstructorId = courseDTO.InstructorId
        };

        await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);
        return course;
    }

    public async Task<CourseResult> UpdateCourse(Guid id,
                                                 [UseFluentValidation] CourseInputType courseInput,
                                                 [Service] ITopicEventSender topicEventSender)
    {
        CourseDTO courseDTO = new()
        {
            Id = id,
            Name = courseInput.Name,
            Subject = courseInput.Subject,
            InstructorId = courseInput.InstructorId
        };

        courseDTO = await _courseRepository.Update(courseDTO);

        CourseResult course = new()
        {
            Id = courseDTO.Id,
            Name = courseDTO.Name,
            Subject = courseDTO.Subject,
            InstructorId = courseDTO.InstructorId
        };

        string updatedCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
        await topicEventSender.SendAsync(updatedCourseTopic, course);

        return course;
    }

    public async Task<bool> DeleteCourse(Guid id)
    {
        try
        {
            return await _courseRepository.Delete(id);
        }
        catch (Exception)
        {
            return false;
        }
    }
}
