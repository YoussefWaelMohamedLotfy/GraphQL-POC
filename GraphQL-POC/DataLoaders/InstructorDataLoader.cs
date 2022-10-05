using GraphQL_POC.DTOs;
using GraphQL_POC.Services.Instructors;

namespace GraphQL_POC.DataLoaders;

public class InstructorDataLoader : BatchDataLoader<Guid, InstructorDTO>
{
    private readonly InstructorRepository _repository;

    public InstructorDataLoader(InstructorRepository repository, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _repository = repository;
    }

    protected override async Task<IReadOnlyDictionary<Guid, InstructorDTO>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        IEnumerable<InstructorDTO> instructors = await _repository.GetManyByIds(keys);

        return instructors.ToDictionary(i => i.Id);
    }
}
