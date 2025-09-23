using Dapper;
using Npgsql;

namespace det_er_fredag.Objects;

public class Person: IDbModel<Person>
{
    public int id { get; set; }
    public string username { get; set; }
    public long discord_id { get; set; }
    public DateTime created_at { get; set; }

    public static Person ReadObj(int id)
    {
        return DatabaseController.GetInstance().db.Query<Person>("SELECT id, username, discord_id, created_at from persons WHERE id = @id", new { id = id }).First();
    }

    public static IEnumerable<Person> GetAll()
    {
        return DatabaseController.GetInstance().db.Query<Person>("SELECT id, username, discord_id, created_at from persons").AsList();
    }

    public static Person ReadObj(long discord_id)
    {
        return DatabaseController.GetInstance().db.Query<Person>("SELECT id, username, discord_id, created_at from persons WHERE discord_id = @discord_id", new { discord_id = discord_id }).First();
    }
    

}