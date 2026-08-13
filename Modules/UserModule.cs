using Fredagsbar.Bot.BackendApi;
using Discord;
using Discord.Interactions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Fredagsbar.Shared.DTO;

namespace Fredagsbar.Bot.Modules;

public class UserModule(ILogger<UserModule> logger, IConfiguration config, BackendClient client) : ModuleBase(config, client)
{
	private readonly ILogger<UserModule> _logger = logger;


	[SlashCommand("register", "Registers you")]
	public async Task Register()
	{
		await RespondAsync("Working...", ephemeral: true);
		var id = Context.User.Id;
		var guildUser = Context.Guild.GetUser(id);
		try
		{
			var user = await _client.UserCreateDtoAsync(new() { ID = id, DisplayName = guildUser.DisplayName, Username = guildUser.Username });
			if (user == null)
			{
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
		}
		catch (Exception e)
		{
			_logger.LogError($"Message: {e.Message}");
			await ModifyOriginalResponseAsync(original => original.Content = "Server error, try again later.");
		}
	}

	[Group("my", "Commands relating to you")]
	public class MeCommandGroupModule(ILogger<UserModule> logger, IConfiguration config, BackendClient client) : ModuleBase(config, client)
	{
		private readonly ILogger<UserModule> _logger = logger;

		[SlashCommand("profile", "Your profile")]
		public async Task MyProfile()
		{
			await DeferAsync();
			UserDto? user;
			try
			{
				user = await this._client.UserGetAsync(Context.User.Id);
			}
			catch (Exception e)
			{
				_logger.LogError($"Message: {e.Message}");
				await ModifyOriginalResponseAsync(o => o.Content = "Server error, try again later.");
				return;
			}
			if (user == null)
			{
				await ModifyOriginalResponseAsync(o => o.Content = "You're not registered yet.\nRegister with `/register`");
				return;
			}

			var builder = new EmbedBuilder
			{
				Title = $"{user.DisplayName} profile",
				ThumbnailUrl = Context.User.GetDisplayAvatarUrl(),
				Color = Color.Blue,
			};

			builder.AddField("Current outstanding debt", user.BeerCasesOwed);
			builder.AddField("Mistakes", 5, true);
			builder.AddField("Paid Off", 1, true);
			builder.WithCurrentTimestamp();

			await ModifyOriginalResponseAsync(o => { o.Content = null; o.Embed = builder.Build(); });
		}

		[SlashCommand("debt", "Your debt statistics")]
		public async Task MyDebt()
		{
			await RespondAsync("Working...", ephemeral: true);
		}
	}
}
