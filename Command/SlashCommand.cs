using Discord;
using Discord.WebSocket;

namespace det_er_fredag.Command;

public abstract class SlashCommand(string name, string description) {
    public string name = name;
    public string description = description;

    internal abstract void Run(SocketSlashCommand command);
    internal virtual SlashCommandBuilder BuildCommand() {
        SlashCommandBuilder slashCommandBuilder = new();
        slashCommandBuilder.WithName(name);
        slashCommandBuilder.WithDescription(description);
        return slashCommandBuilder;
    }
}