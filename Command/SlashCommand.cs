using Discord;
using Discord.WebSocket;

namespace det_er_fredag.Command;

public abstract class SlashCommand {
    public string name;

    public SlashCommand(string name) {
        this.name = name;
    }

    internal abstract void Run(SocketSlashCommand command);
    internal virtual SlashCommandBuilder BuildCommand() {
        SlashCommandBuilder slashCommandBuilder = new();
        slashCommandBuilder.WithName(name);
        return slashCommandBuilder;
    }
}