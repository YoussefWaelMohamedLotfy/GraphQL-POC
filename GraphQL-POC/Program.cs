using AppAny.HotChocolate.FluentValidation;
using FluentValidation;
using GraphQL_POC.Data;
using GraphQL_POC.DataLoaders;
using GraphQL_POC.Schema.Mutations;
using GraphQL_POC.Schema.Queries;
using GraphQL_POC.Schema.Subscriptions;
using GraphQL_POC.Services.Courses;
using GraphQL_POC.Services.Instructors;
using GraphQL_POC.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<CourseTypeInputValidator>();

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<CourseType>()
    .AddType<InstructorType>()
    .AddTypeExtension<CourseQuery>()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddFluentValidation(o =>
    {
        o.UseDefaultErrorMapper();
    });

builder.Services.AddInMemorySubscriptions();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddPooledDbContextFactory<SchoolDbContext>(o => o.UseSqlite(connectionString));

builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<InstructorRepository>();
builder.Services.AddScoped<InstructorDataLoader>();

var app = builder.Build();

app.UseWebSockets();

app.MapGraphQL();

app.Run();
