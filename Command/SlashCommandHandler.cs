using Discord;
using Discord.WebSocket;

namespace det_er_fredag.Command;

public class SlashCommandHandler {
    private DiscordSocketClient? _client;
    private readonly Dictionary<string, SlashCommand> _commands = [];
    private static SlashCommandHandler _singleton = new();

    private SlashCommandHandler() {
        AddCommand(new UpdateCommandsCommand());
        AddCommand(new RouletteCommand());
        AddCommand(new TestCommand());
        AddCommand(new CrateCommand());
    }

    public static SlashCommandHandler GetInstance() {
        _singleton ??= new();
        return _singleton;
    }

    internal void Initialize(DiscordSocketClient client) {
        _client = client;
        client.SlashCommandExecuted += HandleSlashCommand;
    }

    async internal Task HandleSlashCommand(SocketSlashCommand command)   {
        if (_client == null) return; // TODO Add exeption

        // debug
        Console.WriteLine($"Received command: {command.Data.Name}");
        Console.WriteLine("Received options:");
        foreach (var item in command.Data.Options)
        {
            Console.WriteLine($"{item.Name}: {item.Value} ({item.Type})");
        }

        SlashCommand slashCommand = _commands[command.CommandName];
        slashCommand.Run(command);
    }

    internal Dictionary<string, SlashCommand> getCommands() {
        return _commands;
    }

    private void AddCommand(SlashCommand slashCommand) {
        _commands.Add(slashCommand.name, slashCommand);
    }
}

public static class DictionaryStringCommandExtensions {
    public static void Add(this Dictionary<string, SlashCommand> _commands, SlashCommand command) {
        _commands.Add(command.name, command);
    }

    public static void something(this List<SlashCommand> _commands, int x) {
        
    }
}