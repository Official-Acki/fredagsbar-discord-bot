using det_er_fredag.Command;
using Discord;
using Discord.WebSocket;

public class LinkAccountCommand : SlashCommand {
    public LinkAccountCommand() : base("link-account", "Command for linking accounts") { }

    internal override SlashCommandBuilder BuildCommand() {
        var accountCommand = base.BuildCommand();
        accountCommand.AddOption("code", ApplicationCommandOptionType.String, "The code you received to link your account", isRequired: true);

        return accountCommand;
    }

    internal override void Run(SocketSlashCommand command)
    {
        var codeOption = command.Data.Options.FirstOrDefault(x => x.Name == "code");
        if (codeOption != null)
        {
            string code = (string)codeOption.Value;
            // Handle the linking of the account using the code
        }
    }
}