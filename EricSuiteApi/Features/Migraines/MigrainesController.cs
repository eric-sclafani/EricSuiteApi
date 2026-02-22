using EricSuiteApi.Features.Migraines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace EricSuiteApi.Features.Migraines;

[ApiController]
[Route("[controller]/api/[action]")]
public class MigrainesController : ControllerBase
{
	private readonly Container _container;
	public MigrainesController(Database database)
	{
		_container = database.GetContainer("migraine");
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Migraine>>> TestingEndpoint()
	{
		var userId = "eric";
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

		return Ok(results);
	}
}