using Discord;
using Discord.WebSocket;

public class ChannelHandler {
    private readonly DiscordSocketClient _client;
    private readonly IGuild _guild;
    private readonly ulong _categoryid = 1283894063594213488;

    public ChannelHandler(DiscordSocketClient client) {
        _client = client;
        _guild = _client.GetGuild(1278323048356773920);
    }

    public ulong CreateChannel(string name) {
        var channel = _guild.CreateTextChannelAsync(name, x => {
            x.CategoryId = _categoryid;
        });
        while (!channel.IsCompleted) {}
        return channel.Result.Id;
    }

    public async void DeleteChannel(ulong id) {
        var channel = await _guild.GetChannelAsync(id);
        await channel.DeleteAsync();
    }
}
