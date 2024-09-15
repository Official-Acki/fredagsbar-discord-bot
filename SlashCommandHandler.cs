using Discord;
using Discord.WebSocket;

public class SlashCommandHandler
{

    private static DiscordSocketClient? _client;

    internal static void Initialize(DiscordSocketClient client)
    {
        _client = client;
        client.SlashCommandExecuted += HandleSlashCommand;
    }

    internal static async Task HandleSlashCommand(SocketSlashCommand command)   {
        if (_client == null) return;
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
        Console.WriteLine($"Received command: {command.Data.Name}");
        Console.WriteLine("Received options:");
        foreach (var item in command.Data.Options)
        {
            Console.WriteLine($"{item.Name}: {item.Value} ({item.Type})");
        }
        switch (command.Data.Name)  {
            case "update-commands":
                await command.RespondAsync("Updating commands...", ephemeral: true);
                List<SlashCommandBuilder> builders = new List<SlashCommandBuilder>();
                // Roulette
                var rouletteCommand = new SlashCommandBuilder();
                rouletteCommand.WithName("roulette");
                rouletteCommand.WithDescription("Play a game of roulette.");
                rouletteCommand.AddOption("weighted", ApplicationCommandOptionType.Boolean, "Should the roulette be weighted?", false);
                builders.Add(rouletteCommand);

                // new-channel
                var newChannelCommand = new SlashCommandBuilder();
                newChannelCommand.WithName("new-channel");
                newChannelCommand.WithDescription("Creates a new channel.");
                newChannelCommand.AddOption("channel-name", ApplicationCommandOptionType.String, "The name of the channel.", true);
                builders.Add(newChannelCommand);
                try
                {
                    foreach (var item in builders)
                    {
                        await guild.CreateApplicationCommandAsync(item.Build());
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                break;
            case "roulette":
                // Get people who owe crates
                Dictionary<ulong, float> owedCrates = new Dictionary<ulong, float>();
                owedCrates.Add(284800484135665664, 10f);
                owedCrates.Add(641719712882884638, 0.5f);
                owedCrates.Add(313689155638919168, 1f);
                float sum = 0;
                KeyValuePair<ulong, float> selected;
                bool weighted = true;
                if(command.Data.Options.Count > 0 && command.Data.Options.Any(x => x.Name == "weighted")) {
                    weighted = ((bool) command.Data.Options.First(x => x.Name == "weighted").Value);
                }
                Random rnd = new Random();
                if (weighted == true) {
                    Dictionary<ulong, float> converted = new Dictionary<ulong, float>(owedCrates.Count);
                    foreach (var item in owedCrates)
                    {
                        sum += item.Value;
                        converted.Add(item.Key, sum);
                    }
                    var probability = rnd.NextDouble() * sum;
                    selected = converted.SkipWhile(i => i.Value < probability).First();
                } else {
                    selected = owedCrates.ToArray()[(int)Math.Round(rnd.NextDouble() * owedCrates.Count())];
                }
                await command.RespondAsync($"{_client.GetUser(selected.Key).Mention} loooooostt, booohooo! ({Math.Round(owedCrates[selected.Key]/sum, 3)*100}%)");
                break;
            case "new-channel":
                ChannelHandler channelHandler = new ChannelHandler(_client);
                String ?name = command.Data.Options.First().Value.ToString();
                if (name == null) return;
                // var channel = channelHandler.CreateChannel(name);
                Event newEvent = new Event(DateTime.Now, DateTime.Now, "Test", "Test", "Test");
                // newEvent.ChannelId = channel;
                Embed embed = newEvent.GetEmbed();
                await command.Channel.SendMessageAsync("", embed: embed);
                // SocketTextChannel textChannel = guild.GetTextChannel(channel);
                // await textChannel.SendMessageAsync("", embed: embed);
                await command.RespondAsync($"Channel {name} created with id {null}");
                break;
            default:
                break;
        }
        
    }
}