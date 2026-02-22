namespace EricSuiteApi.Infrastructure.Cosmos;

// Base repo interface for feature interfaces to extend
public interface ICosmosRepository<T>
{
	Task<T> GetAsync(string id, string partitionKey);
	Task AddAsync(T entity);
	Task UpdateAsync(T entity);
	Task DeleteAsync(string id, string paritionKey);
}