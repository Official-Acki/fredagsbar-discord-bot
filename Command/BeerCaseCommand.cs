using Discord;
using Discord.WebSocket;

namespace det_er_fredag.Command;

public class BeerCaseCommand : SlashCommand {
    public BeerCaseCommand() : base("beer-cases", "Command for beer cases") {}

    internal override SlashCommandBuilder BuildCommand() {
        var caseCommand = base.BuildCommand();
        caseCommand.AddOption(
            new SlashCommandOptionBuilder()
                .WithName("list")
                .WithDescription("Lists cases owed")
                .WithType(ApplicationCommandOptionType.SubCommand))
        .AddOption(
            new SlashCommandOptionBuilder()
                .WithName("add")
                .WithDescription("Adds specified amounts of cases to user")
                .WithType(ApplicationCommandOptionType.SubCommand)
                .AddOption("amount", ApplicationCommandOptionType.Number, "Amount of cases to add", isRequired:true)
                .AddOption("user", ApplicationCommandOptionType.User, "User to assign cases", isRequired: true)
        )
        .AddOption(
            new SlashCommandOptionBuilder()
                .WithName("remove")
                .WithDescription("Removes cases")
                .WithType(ApplicationCommandOptionType.SubCommand)
                .AddOption("amount", ApplicationCommandOptionType.Number, "Amount of cases to remove", isRequired:true)
                .AddOption("user", ApplicationCommandOptionType.User, "User to assign cases", isRequired: true)
        );
        
        return caseCommand;
    }

    internal override void Run(SocketSlashCommand command)
    {
        throw new NotImplementedException();
    }
}