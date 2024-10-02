
using Discord;
using Discord.WebSocket;

namespace det_er_fredag.Command;

public class RouletteCommand : SlashCommand {
    public RouletteCommand() : base("roulette") {
    }

    internal override SlashCommandBuilder BuildCommand() {
        var rouletteCommand = base.BuildCommand();
        rouletteCommand.WithDescription("Play a game of roulette.");
        rouletteCommand.AddOption("weighted", ApplicationCommandOptionType.Boolean, "Should the roulette be weighted?", false);
        return rouletteCommand;
    }

    async internal override void Run(SocketSlashCommand command) {
        // Get people who owe crates
        Dictionary<ulong, float> owedCrates = new Dictionary<ulong, float>();
        owedCrates.Add(284800484135665664, 1f);
        owedCrates.Add(641719712882884638, 2.5f);
        owedCrates.Add(313689155638919168, 5f);
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
        DiscordSocketClient _client = Program._client;
        await command.RespondAsync($"{_client.GetUser(selected.Key).Mention} loooooostt, booohooo! ({Math.Round(owedCrates[selected.Key]/sum, 3)*100}%)");
    }
}