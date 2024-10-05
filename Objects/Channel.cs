namespace det_er_fredag.Objects;

class Channel {
    int dbId;
    readonly ulong discord_id;
    string name;
    Event belongingEvent; //maybe not needed

    public Channel(ulong discord_id, string name) {
        this.discord_id = discord_id;
        this.name = name;
    }
}