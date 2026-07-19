using System.Net;
using System.Net.Http.Json;
using det_er_fredag.BackendApi.DTO;
using Microsoft.Extensions.Logging;

namespace det_er_fredag.BackendApi;

public class BackendClient(ILogger<BackendClient> logger, HttpClient http)
{
	private readonly ILogger<BackendClient> _logger = logger;
	private readonly HttpClient _http = http;

	// User
	public async Task<int> UserGetAsync(int? id)
	{
		return await _http.GetFromJsonAsync<int>($"users/{id}");
	}

	public async Task<UserCreateDto?> UserCreateDtoAsync(UserCreateDto req, CancellationToken ct = default)
	{
		var response = await _http.PostAsJsonAsync("users", req, ct);
		if (response.StatusCode == HttpStatusCode.Conflict) return null;
		response.EnsureSuccessStatusCode();
		return await response.Content.ReadFromJsonAsync<UserCreateDto>(ct);
	}
}
