using System.Net;
using EricSuiteApi.Features.Migraines.Dtos;
using EricSuiteApi.Features.Migraines.Models;
using EricSuiteApi.Features.Migraines.Repositories;
using EricSuiteApi.Infrastructure.Shared;

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
			id = m.id,
			intensity = m.intensity,
			date = m.date,
			notes = m.notes,
			medications = m.medications
		});
		return dto;
	}

	public async Task<Result<Migraine>> AddMigraine(MigraineDto dto)
	{
		var migraine = new Migraine
		{
			intensity = dto.intensity,
			date = dto.date,
			notes = dto.notes,
			medications = dto.medications
		};

		Result<Migraine> result = new();
		try
		{
			var resp = await _migraineRepo.AddAsync(migraine);
			result.Resource = resp.Resource;
			result.Message = "Migraine added";
		}
		catch (Exception e)
		{
			result.IsSuccess = false;
			result.Message = e.Message;
		}

		return result;
	}
}