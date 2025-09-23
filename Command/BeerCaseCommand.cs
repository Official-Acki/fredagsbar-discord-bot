using Dapper;
using det_er_fredag.Objects;
using Discord;
using Discord.WebSocket;
using System.Data.Common;
using System.Linq;

namespace det_er_fredag.Command;

public class BeerCaseCommand : SlashCommand {
    public BeerCaseCommand() : base("beer-cases", "Command for beer cases") { }

    internal override SlashCommandBuilder BuildCommand() {
        var caseCommand = base.BuildCommand();
        caseCommand
        .AddOption(
            new SlashCommandOptionBuilder()
                .WithName("list")
                .WithDescription("Lists cases owed")
                .WithType(ApplicationCommandOptionType.SubCommand)
                .AddOption("user", ApplicationCommandOptionType.User, "User to list cases for", isRequired: false))
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

    private async void ListCases(SocketSlashCommand command)
    {
        await command.RespondAsync("Listing cases", ephemeral: true);
        
        // Get the "list" subcommand
        var listSubcommand = command.Data.Options.FirstOrDefault(x => x.Name == "list");
        if (listSubcommand?.Options != null && listSubcommand.Options.Any())
        {
            // Check if user option is provided
            var userOption = listSubcommand.Options.FirstOrDefault(x => x.Name == "user");
            if (userOption != null)
            {
                SocketUser? user = (SocketUser?)userOption.Value;
                if (user == null)
                {
                    await command.FollowupAsync("User not found", ephemeral: true);
                    return;
                }
                Person person = Person.ReadObj((long)user.Id);
                Console.WriteLine(person);
                CasesOwed casesOwed = CasesOwed.ReadObj(person.id);
                await command.FollowupAsync($"{user.Username} owes {casesOwed.cases} cases", ephemeral: true);
            }
        }
        else
        {
            // List all cases when no user is specified
            Console.WriteLine("Listing all cases");
        }
    }

    private async void AddCases(SocketSlashCommand command) {
        await command.RespondAsync("Adding cases", ephemeral: true);
    }
    
    private async void RemoveCases(SocketSlashCommand command) {
        await command.RespondAsync("Removing cases", ephemeral: true);
    }
}