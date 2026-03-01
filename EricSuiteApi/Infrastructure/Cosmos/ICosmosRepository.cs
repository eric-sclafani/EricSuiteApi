namespace EricSuiteApi.Infrastructure.Cosmos;

// Base repo interface for feature interfaces to extend
public interface ICosmosRepository<T>
{
	Task<List<T>> GetAllAsync();
	Task AddAsync(T entity);
	Task UpdateAsync(T entity);
	Task DeleteAsync(string id);
}