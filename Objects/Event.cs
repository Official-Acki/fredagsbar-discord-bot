namespace det_er_fredag.Objects;

using Discord;

class Event {
    private int dbId;
    private string name;
    private string description;
    private string location;
    private DateTime startDateTime;
    private DateTime endDateTime;
    private readonly string goingEmoji = ":whitecheckmark:";
    private readonly string notGoingEmoji = ":x:";
    private List<Person> going;
    private List<Person> notGoing;
    private Person creator;
    private Channel channel;

    public Event(int dbId, DateTime startDateTime, DateTime endDateTime, string name, string description, string location, Person creator) {
        this.dbId = dbId;
        this.startDateTime = startDateTime;
        this.endDateTime = endDateTime;
        this.name = name;
        this.description = description;
        this.location = location;
        this.creator = creator;
        going = [];
        notGoing = [];
    }

    public Event(DateTime startDateTime, DateTime endDateTime, string name, string description, string location, Person creator) {
        this.startDateTime = startDateTime;
        this.endDateTime = endDateTime;
        this.name = name;
        this.description = description;
        this.location = location;
        this.creator = creator;
        going = [];
        notGoing = [];
    }

    public Embed GetEmbed() {
        var embed = new EmbedBuilder();
        embed.WithTitle(name);
        embed.WithDescription(description);
        embed.AddField("Location", location);
        embed.AddField("Start Time", startDateTime.ToString());
        embed.AddField("End Time", endDateTime.ToString());
        // embed.AddField("Going", GetGoingString());
        // embed.AddField("Not Going", GetNotGoingString());
        return embed.Build();
    }
}