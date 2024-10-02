
using Discord;
using Discord.WebSocket;

namespace det_er_fredag.Command;

public class TestCommand : SlashCommand {
    public TestCommand() : base("test-command") {
    }

    internal override SlashCommandBuilder BuildCommand() {
        SlashCommandBuilder testCommand = base.BuildCommand();
        testCommand.WithDescription("Creates a new channel.");
        testCommand.AddOption("channel-name", ApplicationCommandOptionType.String, "The name of the channel.", true);
        return testCommand;
    }

    async internal override void Run(SocketSlashCommand command) {
        DiscordSocketClient _client = Program._client;
        ChannelHandler channelHandler = new ChannelHandler(_client);
        String ?name = command.Data.Options.First().Value.ToString();
        if (name == null) return;
        // var channel = channelHandler.CreateChannel(name);
        Event newEvent = new Event(DateTime.Now, DateTime.Now, "Test", "Test", "Test");
        // newEvent.ChannelId = channel;
        Embed embed = newEvent.GetEmbed();
        await command.Channel.SendMessageAsync("", embed: embed);
        // SocketTextChannel textChannel = guild.GetTextChannel(channel);
        // await textChannel.SendMessageAsync("", embed: embed);
        await command.RespondAsync($"Channel {name} created with id {null}");
    }
}