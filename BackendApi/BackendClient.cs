using System.Net;
using System.Net.Http.Json;
using Fredagsbar.Shared.DTO;
using Microsoft.Extensions.Logging;

namespace Fredagsbar.Bot.BackendApi;

public class BackendClient(ILogger<BackendClient> logger, HttpClient http)
{
	private readonly ILogger<BackendClient> _logger = logger;
	private readonly HttpClient _http = http;

	// User
	public async Task<UserDto?> UserGetAsync(ulong id)
	{
		var response = await _http.GetAsync($"users/{id}");
		if (response.StatusCode == HttpStatusCode.NotFound) return null;
		response.EnsureSuccessStatusCode();
		return await response.Content.ReadFromJsonAsync<UserDto>();
	}

	public async Task<UserCreateDto?> UserCreateDtoAsync(UserCreateDto req, CancellationToken ct = default)
	{
		var response = await _http.PostAsJsonAsync("users", req, ct);
		if (response.StatusCode == HttpStatusCode.Conflict) return null;
		response.EnsureSuccessStatusCode();
		return await response.Content.ReadFromJsonAsync<UserCreateDto>(ct);
	}
}
