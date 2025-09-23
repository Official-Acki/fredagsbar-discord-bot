using Npgsql;

namespace det_er_fredag.Objects;

public class Person : DatabaseObj<Person> {
    public int id { get; }
    public string username { get; }
    public UInt64 discord_id { get; }
    public DateTime created_at { get; }


    public Person(int id, string username, UInt64 discord_id, DateTime created_at)
    {
        this.id = id;
        this.username = username;
        this.discord_id = discord_id;
        this.created_at = created_at;
    }


    // Handled through website
    // public void CreateObj() {
    //     string query = "INSERT INTO Person (discord_id, name) VALUES (@discord_id, @name)";
    //     NpgsqlCommand command = new(query, DatabaseController.GetInstance().GetConnection());
    //     command.Parameters.AddWithValue("@discord_id", discord_id);
    //     command.Parameters.AddWithValue("@name", name);
    //     DatabaseController.GetInstance().Query(command);
    // }
    public void CreateObj() {
        throw new Exception("Not allowed to create a person. Handle through website");
    }

    public static List<Person> ReadToObjs(NpgsqlDataReader sqlDataReader)
    {
        throw new NotImplementedException();
    }

    public void UpdateObj() {
        string query = "UPDATE Person SET username = @username, discord_id = @discord_id WHERE id = @id";
        NpgsqlCommand command = new(query, DatabaseController.GetInstance().GetConnection());
        command.Parameters.AddWithValue("@discord_id", discord_id);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@id", id);
        DatabaseController.GetInstance().Query(command);
    }

    public void DeleteObj() {
        throw new Exception("Not allowed to delete a person");
        if (id == null) {
            throw new Exception("Cannot delete object that has not been created in the database");
        }
        string query = "DELETE FROM Person WHERE id = @id";
        NpgsqlCommand command = new(query, DatabaseController.GetInstance().GetConnection());
        command.Parameters.AddWithValue("@id", id);
        DatabaseController.GetInstance().Query(command);
    }
}