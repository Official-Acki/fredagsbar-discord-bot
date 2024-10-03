
using Discord;
using Discord.WebSocket;

namespace det_er_fredag.Command;

public class UpdateCommandsCommand : SlashCommand {
    public UpdateCommandsCommand() : base("update-commands", "Update the commands.") {}

    async internal override void Run(SocketSlashCommand command) {
        var guild = Program._client.GetGuild(1278323048356773920); //TODO
        //update-commands
        await command.RespondAsync("Updating commands...", ephemeral: true);

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
    }
}