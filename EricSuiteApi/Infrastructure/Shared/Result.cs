namespace EricSuiteApi.Infrastructure.Shared;

public class Result<T>
{
	public bool IsSuccess { get; set; } = true;
	public string Message { get; set; }
	public T? Resource { get; set; }
}