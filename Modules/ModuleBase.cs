using Discord.Interactions;
using Fredagsbar.Bot.BackendApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class ModuleBase(IConfiguration config, BackendClient client) : InteractionModuleBase<SocketInteractionContext>
{
	protected readonly IConfiguration _config = config;
	protected readonly BackendClient _client = client;

	protected string GetDisplayName() {
		var id = Context.User.Id;
		var guildUser = Context.Guild.GetUser(id);
		return guildUser.DisplayName;
	}
}
