using GraphQL_POC.Data;
using GraphQL_POC.Schema.Mutations;
using GraphQL_POC.Schema.Queries;
using GraphQL_POC.Schema.Subscriptions;
using GraphQL_POC.Services.Courses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>();

builder.Services.AddInMemorySubscriptions();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddPooledDbContextFactory<SchoolDbContext>(o => o.UseSqlite(connectionString));

builder.Services.AddScoped<CourseRepository>();

var app = builder.Build();

app.UseWebSockets();

app.MapGraphQL();

app.Run();
