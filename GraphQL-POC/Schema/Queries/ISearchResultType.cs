using HotChocolate.Types;

namespace GraphQL_POC.Schema.Queries;

[InterfaceType("SearchResult")]
public interface ISearchResultType
{
    Guid Id { get; set; }
}
