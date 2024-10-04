using Discord;
using Discord.WebSocket;

namespace det_er_fredag.Command;

public class CratesCommands : SlashCommand {
    public CratesCommands() : base("crates","Command for crates") {}

    internal override SlashCommandBuilder BuildCommand() {
        var cratesCommand = base.BuildCommand();
        cratesCommand.AddOption(
            new SlashCommandOptionBuilder()
                .WithName("list")
                .WithDescription("lists crates owed")
                .WithType(ApplicationCommandOptionType.SubCommand))
        .AddOption(
            new SlashCommandOptionBuilder()
                .WithName("add")
                .WithDescription("adds specified amounts of crates to user")
                .WithType(ApplicationCommandOptionType.SubCommand)
                .AddOption("amount", ApplicationCommandOptionType.Number, "Amount of crates to add", isRequired:true)
                .AddOption("user", ApplicationCommandOptionType.User, "user to assign crates", isRequired: true)
        )
        .AddOption(
            new SlashCommandOptionBuilder()
                .WithName("remove")
                .WithDescription("removes crates")
                .WithType(ApplicationCommandOptionType.SubCommand)
                .AddOption("amount", ApplicationCommandOptionType.Number, "Amount of crates to remove", isRequired:true)
                .AddOption("user", ApplicationCommandOptionType.User, "user to assign crates", isRequired: true)
        );
        
        return cratesCommand;
    }

    internal override void Run(SocketSlashCommand command)
    {
        throw new NotImplementedException();
    }
}