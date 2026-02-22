using EricSuiteApi.Features.Migraines.Models;
using EricSuiteApi.Infrastructure.Cosmos;
using Microsoft.Azure.Cosmos;

namespace EricSuiteApi.Features.Migraines.Repositories;

public class MigraineRepository 
{
	private Container _container;

	public MigraineRepository(Database database)
	{
		_container = database.GetContainer("migraine");
	}

	// public async Task<Migraine[]> GetAsync(string id, string partitionKey)
	// {
	// 	
	// 	
	// }
}