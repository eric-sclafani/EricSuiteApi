using System.Net;
using System.Text.Json;
using Microsoft.Azure.Cosmos;

namespace EricSuiteApi.Infrastructure.Middleware;


public class ErrorHandlingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ErrorHandlingMiddleware> _logger;

	public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception e)
		{
			await HandleExceptionAsync(context, e);
			_logger.LogError(e, "Unhandled exception occurred.");
		}
	}

	private static async Task HandleExceptionAsync(HttpContext context, Exception e)
	{
		
		var code = HttpStatusCode.InternalServerError;
		var errorMessage = "Something went wrong.";

		switch (e)
		{
			case CosmosException cosmosEx when cosmosEx.StatusCode == HttpStatusCode.NotFound:
				code = HttpStatusCode.NotFound;
				errorMessage = "Item not found with provided id.";
				break;
			case CosmosException cosmosEx when cosmosEx.StatusCode == HttpStatusCode.Conflict:
				code = HttpStatusCode.Conflict;
				errorMessage = "Item already exists with given id.";
				break;

			case CosmosException cosmosEx when cosmosEx.StatusCode == HttpStatusCode.TooManyRequests:
				code = HttpStatusCode.TooManyRequests;
				errorMessage = "Request rate too large.";
				break;
			case CosmosException cosmosEx when cosmosEx.StatusCode == HttpStatusCode.BadRequest:
				code = HttpStatusCode.BadRequest;
				errorMessage = "Malformed request. Check request body for incorrect/missing fields.";
				break;
			
		}

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)code;
		
		var errorResponse = new ApiErrorResponse((int)code, errorMessage);
		var serializedErrorResponse = JsonSerializer.Serialize(errorResponse);
		
		await context.Response.WriteAsync(serializedErrorResponse);

	}
}

internal class ApiErrorResponse
{
	public string? ErrorMessage { get; set; }

	public int StatusCode { get; set; }

	public ApiErrorResponse(int code, string errorMessage)
	{
		ErrorMessage = errorMessage;
		StatusCode = code;
	}
}
