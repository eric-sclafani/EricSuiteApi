using System.Net;
using EricSuiteApi.Features.Migraines.Dtos;
using EricSuiteApi.Features.Migraines.Models;
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
			id = m.id,
			intensity = m.intensity,
			date = m.date,
			notes = m.notes,
			medications = m.medications
		});
		return dto;
	}

	public async Task<Migraine> AddMigraine(MigraineDto dto)
	{
		var migraine = Migraine.FromDto(dto);
		var resp = await _migraineRepo.AddAsync(migraine);
		return resp.Resource;

	}
	
	public async Task<Migraine> UpdateMigraine(MigraineDto dto)
	{
		var migraine = Migraine.FromDto(dto);
		var resp = await _migraineRepo.UpdateAsync(migraine);
		return resp.Resource;
	}

	public async Task<Migraine> DeleteMigraine(string id)
	{
		var resp = await _migraineRepo.DeleteAsync(id);
		return resp.Resource;
	}
}