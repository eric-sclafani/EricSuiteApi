using EricSuiteApi.Features.Migraines.Dtos;
using EricSuiteApi.Features.Migraines.Repositories;

namespace EricSuiteApi.Features.Migraines.Services;

public class MigrainesService
{
	private readonly IMigraineRepository _migraineRepo;
	public MigrainesService(IMigraineRepository migraineRepo)
	{
		_migraineRepo = migraineRepo;
	}

	public async Task<IEnumerable<MigraineDto>> GetMigraines()
	{
		var migraines = await _migraineRepo.GetAllAsync();
		var dto = migraines.Select(m => new MigraineDto
		{
			intensity = m.intensity,
			date = m.date,
			notes = m.notes,
			medications = m.medications
		});
		return dto;
	} 
}