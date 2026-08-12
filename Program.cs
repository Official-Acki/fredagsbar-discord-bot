using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Discord.Interactions;
using Fredagsbar.Bot.BackendApi;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
	.AddEnvironmentVariables()
	.AddJsonFile("appsettings.json")
	.AddJsonFile("appsettings.Development.json", optional: true);

var token = builder.Configuration["BOT_TOKEN"] ?? throw new InvalidOperationException("No bot token in env.");

// Discord Bot Setup
builder.Services.AddSingleton(new DiscordSocketConfig
{
	MessageCacheSize = 100,
	GatewayIntents = GatewayIntents.GuildMembers | GatewayIntents.MessageContent | GatewayIntents.AllUnprivileged,
	UseInteractionSnowflakeDate = false
});

builder.Services.AddSingleton<DiscordSocketClient>();

builder.Services.AddSingleton(new InteractionServiceConfig
{
	DefaultRunMode = RunMode.Async,
	UseCompiledLambda = true
});

builder.Services.AddSingleton(sp =>
    new InteractionService(
        sp.GetRequiredService<DiscordSocketClient>(),
        sp.GetRequiredService<InteractionServiceConfig>()));

builder.Services.AddHostedService<BotWorker>();

// Http Client
var internalHost = builder.Configuration["Internal:Host"] ?? throw new InvalidOperationException("Internal:Host not configured");
var internalApiKey = builder.Configuration["Internal:ApiKey"] ?? throw new InvalidOperationException("Internal:ApiKey not configured");
builder.Services.AddHttpClient<BackendClient>(client =>
{
	client.BaseAddress = new Uri($"http://{internalHost}:5293/");
    client.DefaultRequestHeaders.Add("X-Internal-Key", internalApiKey);
});

var host = builder.Build();
await host.RunAsync();
