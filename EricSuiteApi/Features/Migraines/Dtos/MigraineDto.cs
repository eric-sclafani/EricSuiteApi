using EricSuiteApi.Features.Migraines.Models;

namespace EricSuiteApi.Features.Migraines.Dtos;

public class MigraineDto
{
	public int intensity { get; set; }
	public string date { get; set; }
	public string notes { get; set; }
	public Medication[] medications { get; set; }
}