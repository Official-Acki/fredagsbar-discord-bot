using Discord;

class Event {
    private ulong _channelId {get; set;}
    private DateTime _startDateTime;
    private DateTime _endDateTime;
    private string _name;
    private string _description;
    private string _location;
    private ulong _creatorId;
    private List<ulong> _going;
    private List<ulong> _notGoing;
    private readonly string goingEmoji = ":white_check_mark:";
    private readonly string notGoingEmoji = ":x:";

    public Event(DateTime startDateTime, DateTime endDateTime, string name, string description, string location, ulong creatorId = 0) {
        _startDateTime = startDateTime;
        _endDateTime = endDateTime;
        _name = name;
        _description = description;
        _location = location;
        _creatorId = creatorId;
        _going = [];
        _notGoing = [];
    }

    public Embed GetEmbed() {
        var embed = new EmbedBuilder();
        embed.WithTitle(_name);
        embed.WithDescription(_description);
        embed.AddField("Location", _location);
        embed.AddField("Start Time", _startDateTime.ToString());
        embed.AddField("End Time", _endDateTime.ToString());
        // embed.AddField("Going", GetGoingString());
        // embed.AddField("Not Going", GetNotGoingString());
        return embed.Build();
    }
}