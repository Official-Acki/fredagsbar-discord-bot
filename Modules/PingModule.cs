using Discord.Interactions;

namespace det_er_fredag.Modules;

public class PingModule : InteractionModuleBase<SocketInteractionContext>
{
	[SlashCommand("ping", "Replies with pong")]
	public async Task Ping()
	{
		// Context.Interaction.CreatedAt
		await RespondAsync("Pong!");
		var time = (Context.Interaction.CreatedAt.DateTime - new DateTime()).Milliseconds;
		await ModifyOriginalResponseAsync(original => original.Content = $"Pong! ({time}ms)");
	}
}
