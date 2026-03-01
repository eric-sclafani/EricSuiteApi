using EricSuiteApi.Features.Migraines.Dtos;
using EricSuiteApi.Features.Migraines.Services;
using Microsoft.AspNetCore.Mvc;

namespace EricSuiteApi.Features.Migraines;

[ApiController]
[Route("[controller]/api/[action]")]
public class MigrainesController : ControllerBase
{
	private readonly MigrainesService _migrainesService;
	public MigrainesController(MigrainesService migrainesService)
	{
		_migrainesService = migrainesService;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<MigraineDto>>> GetAllMigraines()
	{
		var migraines = await _migrainesService.GetMigraines();
		return Ok(migraines);
	}
}