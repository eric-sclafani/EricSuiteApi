using EricSuiteApi.Features.Migraines.Models;
using Microsoft.Azure.Cosmos;

namespace EricSuiteApi.Features.Migraines.Repositories;

public class MigraineRepository : IMigraineRepository
{
	private readonly Container _container;
	private readonly string _userId;
	private readonly PartitionKey _partitionKey;

	public MigraineRepository(Database database, IConfiguration config)
	{
		_container = database.GetContainer("migraine");
		_userId = config["Cosmos:UserId"]!;
		_partitionKey = new PartitionKey(_userId);
	}

	public async Task<List<Migraine>> GetAllAsync()
	{
		var query = new QueryDefinition("SELECT * FROM c WHERE c.userId = @userId")
			.WithParameter("@userId", _userId);
		
		var iterator = _container.GetItemQueryIterator<Migraine>(
			query,
			requestOptions: new QueryRequestOptions
			{
				PartitionKey = _partitionKey
			});

		List<Migraine> results = [];

		while (iterator.HasMoreResults)
		{
			var response = await iterator.ReadNextAsync();
			results.AddRange(response.Resource);
		}

		return results;
	}

	public async Task<ItemResponse<Migraine>> AddAsync(Migraine migraine)
	{
		
		var req = await _container.CreateItemAsync(migraine, _partitionKey);
		return req;
	}

	public async Task<ItemResponse<Migraine>> UpdateAsync(Migraine migraine)
	{
		var req = await _container.ReplaceItemAsync(migraine, migraine.id, _partitionKey);
		return req;
	}

	public async Task<ItemResponse<Migraine>> DeleteAsync(string id)
	{
		var req = await _container.DeleteItemAsync<Migraine>(id, _partitionKey);
		return req;
	}
}