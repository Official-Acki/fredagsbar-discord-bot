using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public sealed class BotWorker(
	DiscordSocketClient client,
	InteractionService interactions,
	IServiceProvider services,
	ILogger<BotWorker> logger,
	IConfiguration config) : IHostedService
{
	private readonly DiscordSocketClient _client = client;
	private readonly InteractionService _interactions = interactions;
	private readonly IServiceProvider _services = services;
	private readonly ILogger<BotWorker> _logger = logger;
	private readonly IConfiguration _config = config;

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		_client.Log += LogAsync;
		_interactions.Log += LogAsync;
		_client.Ready += Ready;

		_client.InteractionCreated += async interaction =>
		{
			var ctx = new SocketInteractionContext(_client, interaction);
			await _interactions.ExecuteCommandAsync(ctx, _services);
		};

		var token = _config["BOT_TOKEN"];
		await _client.LoginAsync(TokenType.Bot, token);
		await _client.StartAsync();
	}

	public async Task StopAsync(CancellationToken cancellationToken)
	{
		await _client.StopAsync();
		await _client.LogoutAsync();
	}

	private Task LogAsync(LogMessage msg)
	{
		if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation("{Source}: {Message}", msg.Source, msg.Message);

		return Task.CompletedTask;
	}


	// Events
	private async Task Ready()
	{
		await _interactions.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

		var testGuildId = _config.GetValue<ulong?>("BOT_GUILD_ID");
		if (testGuildId.HasValue)
			await _interactions.RegisterCommandsToGuildAsync(testGuildId.Value);
		// else
		// 	await _interactions.RegisterCommandsGloballyAsync();

		_logger.LogInformation("Slash commands registered.");
	}
}
