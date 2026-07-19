using det_er_fredag.BackendApi;
using Discord;
using Discord.Interactions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace det_er_fredag.Modules;

public class UserModule(ILogger<UserModule> logger, IConfiguration config, BackendClient client) : InteractionModuleBase<SocketInteractionContext>
{
	private readonly ILogger<UserModule> _logger = logger;
	private readonly IConfiguration _config = config;
	private readonly BackendClient _client = client;


	[SlashCommand("register", "Registers you")]
	public async Task Register()
	{
		await RespondAsync("Working...", ephemeral: true);
		var id = Context.User.Id;
		var guildUser = Context.Guild.GetUser(id);
		try {
			var user = await _client.UserCreateDtoAsync(new() { ID = id, DisplayName = guildUser.DisplayName, Username = guildUser.Username });
			if (user == null) {
				await ModifyOriginalResponseAsync(original => original.Content = "Already registered.");
				return;
			}
			var builder = new EmbedBuilder
			{
				Title = $"Welcome {user.DisplayName}",
				Description = "Get to drinking!",
				Color = Color.Blue
			};

			await ModifyOriginalResponseAsync(original => { original.Content = null; original.Embed = builder.Build(); });
		} catch (Exception e) {
			_logger.LogError($"Message: {e.Message}");
			await ModifyOriginalResponseAsync(original => original.Content = "Server error, try again later.");
		}
	}
}
