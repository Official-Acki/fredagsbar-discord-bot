using Discord.Interactions;

namespace det_er_fredag.Modules;

public class PingModule : InteractionModuleBase<SocketInteractionContext>
{
	[SlashCommand("ping", "Replies with pong")]
	public async Task Ping() => await RespondAsync("Pong!");
}
