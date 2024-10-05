using MySqlConnector;

namespace det_er_fredag.Objects;

public class Person : DatabaseObj<Person> {
    readonly int? dbId;
    readonly ulong discord_id;
    string name;


    public Person(ulong discord_id, string name) {
        this.discord_id = discord_id;
        this.name = name;
    }

    public Person(int dbId, ulong discord_id, string name) : this(discord_id, name) {
        this.dbId = dbId;
    }


    public void CreateObj() {
        string query = "INSERT INTO Person (discord_id, name) VALUES (@discord_id, @name)";
        MySqlCommand command = new(query, DatabaseController.GetInstance().GetConnection());
        command.Parameters.AddWithValue("@discord_id", discord_id);
        command.Parameters.AddWithValue("@name", name);
        DatabaseController.GetInstance().Query(command);
    }

    public static List<Person> ReadToObjs(MySqlDataReader mySqlDataReader) {
        throw new NotImplementedException();
    }

    public void UpdateObj() {
        if (dbId == null) {
            throw new Exception("Cannot update object that has not been created in the database");
        }
        string query = "UPDATE Person SET discord_id = @discord_id, name = @name WHERE id = @id";
        MySqlCommand command = new(query, DatabaseController.GetInstance().GetConnection());
        command.Parameters.AddWithValue("@discord_id", discord_id);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@id", dbId);
        DatabaseController.GetInstance().Query(command);
    }

    public void DeleteObj() {
        throw new Exception("Not allowed to delete a person");
        if (dbId == null) {
            throw new Exception("Cannot delete object that has not been created in the database");
        }
        string query = "DELETE FROM Person WHERE id = @id";
        MySqlCommand command = new(query, DatabaseController.GetInstance().GetConnection());
        command.Parameters.AddWithValue("@id", dbId);
        DatabaseController.GetInstance().Query(command);
    }
}