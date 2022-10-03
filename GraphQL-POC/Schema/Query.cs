namespace GraphQL_POC.Schema;

public class Query
{
    [GraphQLDeprecated("It got old.")]
    public string Instructions => "Hello from .NET";
}
