using EricSuiteApi.Features.Migraines.Models;
using EricSuiteApi.Infrastructure.Cosmos;

namespace EricSuiteApi.Features.Migraines.Repositories;

public interface IMigraineRepository : ICosmosRepository<Migraine>
{
	
}