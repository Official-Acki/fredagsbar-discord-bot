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
        switch (command.Data.Name)  {
            case "update-commands":
                await command.RespondAsync("Updating commands...");
                // Roulette
                var rouletteCommand = new SlashCommandBuilder();
                rouletteCommand.WithName("roulette");
                rouletteCommand.WithDescription("Play a game of roulette.");
                var options = new SlashCommandOptionBuilder();
                options.AddOption("weighted", ApplicationCommandOptionType.Boolean, "Should the roulette be weighted?", false);
                rouletteCommand.AddOptions(options);
                try
                {
                    await guild.CreateApplicationCommandAsync(rouletteCommand.Build());
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
                Dictionary<ulong, float> converted = new Dictionary<ulong, float>(owedCrates.Count);
                foreach (var item in owedCrates)
                {
                    sum += item.Value;
                    converted.Add(item.Key, sum);
                }
                Random rnd = new Random();
                var probability = rnd.NextDouble() * sum;
                var selected = converted.SkipWhile(i => i.Value < probability).First();
                await command.RespondAsync($"{_client.GetUser(selected.Key).Mention} loooooostt, booohooo! ({Math.Round(owedCrates[selected.Key]/sum, 3)*100}%)");
                break;
            default:
                break;
        }
        
    }
}