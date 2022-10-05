using FluentValidation;
using GraphQL_POC.Schema.Mutations;

namespace GraphQL_POC.Validators;

public class CourseTypeInputValidator : AbstractValidator<CourseInputType>
{
	public CourseTypeInputValidator()
	{
		RuleFor(c => c.Name)
			.MinimumLength(3)
			.MaximumLength(50)
			.WithMessage("Course name must be between 3 and 50 characters")
			.WithErrorCode("COURSE_NAME_LENGTH");
	}
}
