using EricSuiteApi.Features.Migraines.Dtos;

namespace EricSuiteApi.Features.Migraines.Models;

public class Migraine
{
	public string id { get; set; }
	public string userId { get; set; }
	public int intensity { get; set; }
	public string date { get; set; }
	public string notes { get; set; }
	public Medication[] medications { get; set; }

	public static Migraine FromDto(MigraineDto dto)
	{
		var migraine = new Migraine
		{
			id = dto.id,
			intensity = dto.intensity,
			date = dto.date,
			notes = dto.notes,
			medications = dto.medications
		};
		return migraine;
	}
}