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
	public async Task<ActionResult<IEnumerable<MigraineDto>>> GetAll()
	{
		var migraines = await _migrainesService.GetMigraines();
		return Ok(migraines);
	}

	[HttpPost]
	public async Task<IActionResult> AddNew(MigraineDto migraine)
	{
		var result = await _migrainesService.AddMigraine(migraine);
		return Ok(result);
	}
	
	[HttpPost]
	public async Task<IActionResult> Update(MigraineDto migraine)
	{
		var result = await _migrainesService.UpdateMigraine(migraine);
		return Ok(result);
	}

	[HttpDelete]
	public async Task<IActionResult> Delete(string id)
	{
		var result = await _migrainesService.DeleteMigraine(id);
		return Ok(result);
	}
}