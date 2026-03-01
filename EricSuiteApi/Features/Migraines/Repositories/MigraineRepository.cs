using EricSuiteApi.Features.Migraines.Models;
using Microsoft.Azure.Cosmos;

namespace EricSuiteApi.Features.Migraines.Repositories;

public class MigraineRepository : IMigraineRepository
{
	private readonly Container _container;
	private readonly IConfiguration _config;

	public MigraineRepository(Database database, IConfiguration config)
	{
		_container = database.GetContainer("migraine");
		_config = config;
	}

	public async Task<List<Migraine>> GetAllAsync()
	{
		var userId = _config["Cosmos:UserId"];
		var query = new QueryDefinition("SELECT * FROM c WHERE c.userId = @userId")
			.WithParameter("@userId", userId);
		
		var iterator = _container.GetItemQueryIterator<Migraine>(
			query,
			requestOptions: new QueryRequestOptions
			{
				PartitionKey = new PartitionKey(userId)
			});

		List<Migraine> results = [];

		while (iterator.HasMoreResults)
		{
			var response = await iterator.ReadNextAsync();
			results.AddRange(response.Resource);
		}

		return results;

	}

	public async Task AddAsync(Migraine migraine)
	{
		
	}

	public async Task UpdateAsync(Migraine entity)
	{
		
	}

	public async Task DeleteAsync(string id)
	{
		
	}
}