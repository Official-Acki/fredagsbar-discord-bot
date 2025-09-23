using System.Data;
using System.Dynamic;
using Npgsql;

class DatabaseController {
    readonly string connectionString;
    readonly NpgsqlConnection connection;
    private static DatabaseController _singleton = new();

    public IDbConnection db { get; }

    private DatabaseController()
    {
        string Server = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        string UserID = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
        string Password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "example";
        string Database = Environment.GetEnvironmentVariable("DB_NAME") ?? "beerbotdb";
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = Server,
            Username = UserID,
            Password = Password,
            Database = Database,
            SslMode = SslMode.Prefer,
        };


        connectionString = builder.ConnectionString;

        Console.WriteLine($"Connection string: {connectionString}");
        connection = new(connectionString);
        db = new NpgsqlConnection(connectionString);
    }

    public NpgsqlDataReader? Select(NpgsqlCommand command) {
        NpgsqlDataReader? reader = null;
        try {
            connection.Open();
            reader = command.ExecuteReader();
        }
        catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally {
            connection.Close();
        }
        return reader;
    }

    // TODO add return id of inserted row

    public void Query(NpgsqlCommand command) {
        try {
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally {
            connection.Close();
        }
    }
    
    public static NpgsqlDataReader? ExecuteCommand(NpgsqlCommand command)
    {
        return command.ExecuteReader();
        NpgsqlDataReader? reader;
        try { reader = command.ExecuteReader(); }
        catch (Exception ex)
        {
            Console.WriteLine("Error executing query: " + ex.Message + "\n" + ex.StackTrace);
            // command.Connection.CloseAsync();
            return null; // Query failed
        }
        return reader;
    }

    public NpgsqlConnection GetConnection()
    {
        return connection;
    }

    public NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(connectionString);
    }

    public static DatabaseController GetInstance()
    {
        _singleton ??= new();
        return _singleton;
    }
}