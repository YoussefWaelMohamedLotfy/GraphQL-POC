using GraphQL_POC.Schema.Mutations;
using GraphQL_POC.Schema.Queries;
using GraphQL_POC.Schema.Subscriptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>();

builder.Services.AddInMemorySubscriptions();

var app = builder.Build();

app.UseWebSockets();

app.MapGraphQL();

app.Run();
