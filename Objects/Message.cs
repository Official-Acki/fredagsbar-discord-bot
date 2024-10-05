namespace det_er_fredag.Objects;

class Message {
    int dbId;
    readonly ulong discord_id;
    DateTime timestamp;
    string text;
    Person author;
    Channel channel;

    public Message(ulong discord_id, DateTime timestamp, string text, Person author, Channel channel) {
        this.discord_id = discord_id;
        this.timestamp = timestamp;
        this.text = text;
        this.author = author;
        this.channel = channel;
    }
}