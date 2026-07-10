using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Discord.Interactions;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddEnvironmentVariables();

var token = builder.Configuration["BOT_TOKEN"] ?? throw new InvalidOperationException("No bot token in env.");

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

var host = builder.Build();
await host.RunAsync();
