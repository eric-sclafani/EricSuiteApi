using Microsoft.Azure.Cosmos;

namespace EricSuiteApi.Infrastructure.Cosmos;

// Base repo interface for feature interfaces to extend
public interface ICosmosRepository<T>
{
	Task<List<T>> GetAllAsync();
	Task <ItemResponse<T>>AddAsync(T entity);
	Task <ItemResponse<T>>UpdateAsync(T entity);
	Task <ItemResponse<T>>DeleteAsync(string id);
}