﻿using GraphQL_POC.Schema.Queries;

namespace GraphQL_POC.Schema.Mutations;

public class CourseInputType
{
    public string Name { get; set; }

    public Subject Subject { get; set; }

    public Guid InstructorId { get; set; }
}