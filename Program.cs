using det_er_fredag.Command;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

class Program {
    public static DiscordSocketClient? _client;

    public static async Task Main() {
        var _config = new DiscordSocketConfig { MessageCacheSize = 100, GatewayIntents = GatewayIntents.All};
        _client = new DiscordSocketClient(_config);

        _client.Log += Log;

        var token = File.ReadAllText("token.txt");

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();


        SlashCommandHandler.GetInstance().Initialize(_client);

        _client.MessageUpdated += MessageUpdated;
        _client.ReactionAdded += ReactionAdded;
        _client.MessageReceived += async (msg) => {
            if (msg.Author.IsBot) {
                return;
            }
            if (msg.Content == "ping") {
                await msg.Channel.SendMessageAsync("pong");
            }

            if (msg.Content.ToLower().Contains("øl")) {
                await msg.Channel.SendMessageAsync("ØL:beer:, NOGEN DER SAGDE ØL:beer:, SKAL DER DRIKKES ØL???:beer:");
            }
        };
        _client.Ready += () => 
        {
            var guild = _client.GetGuild(1278323048356773920);
            // Don't uncomment, only to be used once when needed.
            // var guildCommand = new SlashCommandBuilder();
            // guildCommand.WithName("update-commands");
            // guildCommand.WithDescription("Update the commands.");
            // try
            // {
            //     guild.CreateApplicationCommandAsync(guildCommand.Build());
                
            // }
            // catch (System.Exception ex)
            // {
            //     Console.WriteLine(ex.Message);
            // }
            Console.WriteLine("Bot is connected!");
            return Task.CompletedTask;
        };

        CommandService commandService = new CommandService();
        

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }


    private static Task Log(LogMessage msg) {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    private static async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
    {
        // If the message was not in the cache, downloading it will result in getting a copy of `after`.
        Console.WriteLine($"{before.Value}");
        var message = await before.GetOrDownloadAsync();  
        // Console.WriteLine($"{message.Reactions.Count} -> {after.Reactions.Count}");
        Console.WriteLine($"{message.Content} -> {after.Content}");
        // Re cache the message
        await message.Channel.GetMessageAsync(message.Id);
    }

    private static async Task ReactionAdded(Cacheable<IUserMessage, ulong> cacheable1, Cacheable<IMessageChannel, ulong> cacheable2, SocketReaction reaction)
    {
        Console.WriteLine($"{reaction.Emote.Name} -> {reaction.User.Value.Username}");
    }
}