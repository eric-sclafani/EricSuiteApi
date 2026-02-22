namespace EricSuiteApi.Infrastructure.Cosmos;

using Microsoft.Azure.Cosmos;

public static class CosmosClientFactory
{
	public static IServiceCollection AddCosmos(this IServiceCollection services, IConfiguration config)
	{
		var connectionString = config["Cosmos:ConnectionString"];
		var databaseName = config["Cosmos:DatabaseName"];

		var cosmosClient = new CosmosClient(connectionString, new CosmosClientOptions
		{
			ApplicationName = "ericsuite-cosmosdb",
			AllowBulkExecution = true
		});

		services.AddSingleton(cosmosClient);
		services.AddSingleton(sp => cosmosClient.GetDatabase(databaseName));

		return services;
	}
}
