using det_er_fredag.Objects;
using Discord;
using Discord.WebSocket;

namespace det_er_fredag.Command;

public class BeerCaseCommand : SlashCommand {
    public BeerCaseCommand() : base("beer-cases", "Command for beer cases") { }

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
                .AddOption("amount", ApplicationCommandOptionType.Number, "Amount of cases to add", isRequired: true)
                .AddOption("user", ApplicationCommandOptionType.User, "User to assign cases", isRequired: true)
        )
        .AddOption(
            new SlashCommandOptionBuilder()
                .WithName("remove")
                .WithDescription("Removes cases")
                .WithType(ApplicationCommandOptionType.SubCommand)
                .AddOption("amount", ApplicationCommandOptionType.Number, "Amount of cases to remove", isRequired: true)
                .AddOption("user", ApplicationCommandOptionType.User, "User to assign cases", isRequired: true)
        );

        return caseCommand;
    }

    internal override void Run(SocketSlashCommand command) {
        switch (command.Data.Options.First().Name)
        {
            case "list":
                ListCases(command);
                break;
            case "add":
                AddCases(command);
                break;
            case "remove":
                RemoveCases(command);
                break;
            default:
                break;
        }
    }

    private async void ListCases(SocketSlashCommand command) {
        await command.RespondAsync("Listing cases", ephemeral: true);
        BeerCase.ReadToObjs(DatabaseController.GetInstance().Select(new("SELECT * FROM cases_given", DatabaseController.GetInstance().GetConnection())));
    }

    private async void AddCases(SocketSlashCommand command) {
        await command.RespondAsync("Adding cases", ephemeral: true);
    }
    
    private async void RemoveCases(SocketSlashCommand command) {
        await command.RespondAsync("Removing cases", ephemeral: true);
    }
}