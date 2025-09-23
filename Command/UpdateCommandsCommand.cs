
using Discord;
using Discord.WebSocket;

namespace det_er_fredag.Command;

public class UpdateCommandsCommand : SlashCommand {
    public UpdateCommandsCommand() : base("update-commands", "Update the commands.") {}

    async internal override void Run(SocketSlashCommand command) {
        SocketGuild guild = Program._client.GetGuild(ulong.Parse(Environment.GetEnvironmentVariable("GUILD_ID") ?? "1278323048356773920"));
        //update-commands
        await command.RespondAsync("Updating commands...", ephemeral: true);
        await guild.DeleteApplicationCommandsAsync();

        Dictionary<string, SlashCommand> commands = SlashCommandHandler.GetInstance().getCommands();
        foreach (SlashCommand command1 in commands.Values) {
            SlashCommandBuilder builder = command1.BuildCommand();
            try {
                await guild.CreateApplicationCommandAsync(builder.Build());
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return;
            }
        }
        await command.ModifyOriginalResponseAsync(msg => msg.Content = "Commands updated!!!");
    }
}