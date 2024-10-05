using Discord;
using Discord.WebSocket;

namespace det_er_fredag.Command;

public class CrateCommand : SlashCommand {
    public CrateCommand() : base("crates","Command for crates") {}

    internal override SlashCommandBuilder BuildCommand() {
        var crateCommand = base.BuildCommand();
        crateCommand.AddOption(
            new SlashCommandOptionBuilder()
                .WithName("list")
                .WithDescription("Lists crates owed")
                .WithType(ApplicationCommandOptionType.SubCommand))
        .AddOption(
            new SlashCommandOptionBuilder()
                .WithName("add")
                .WithDescription("Adds specified amounts of crates to user")
                .WithType(ApplicationCommandOptionType.SubCommand)
                .AddOption("amount", ApplicationCommandOptionType.Number, "Amount of crates to add", isRequired:true)
                .AddOption("user", ApplicationCommandOptionType.User, "User to assign crates", isRequired: true)
        )
        .AddOption(
            new SlashCommandOptionBuilder()
                .WithName("remove")
                .WithDescription("Removes crates")
                .WithType(ApplicationCommandOptionType.SubCommand)
                .AddOption("amount", ApplicationCommandOptionType.Number, "Amount of crates to remove", isRequired:true)
                .AddOption("user", ApplicationCommandOptionType.User, "User to assign crates", isRequired: true)
        );
        
        return crateCommand;
    }

    internal override void Run(SocketSlashCommand command)
    {
        throw new NotImplementedException();
    }
}