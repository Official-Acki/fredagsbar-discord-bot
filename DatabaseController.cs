using MySqlConnector;

class DatabaseController {
    readonly string connectionString;
    readonly MySqlConnection connection;
    private static DatabaseController _singleton = new();

    private DatabaseController() {
        string Server = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        string UserID = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
        string Password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "example";
        string Database = Environment.GetEnvironmentVariable("DB_NAME") ?? "beerbotdb";
        var builder = new MySqlConnectionStringBuilder {
            Server = Server,
            UserID = UserID,
            Password = Password,
            Database = Database,
            // SslMode = MySqlSslMode.None,
        };


        connectionString = builder.ConnectionString;

        Console.WriteLine($"Connection string: {connectionString}");
        connection = new(connectionString);
    }

    public MySqlDataReader? Select(MySqlCommand command) {
        MySqlDataReader? reader = null;
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

    public void Query(MySqlCommand command) {
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

    public MySqlConnection GetConnection() {
        return connection;
    }

    public static DatabaseController GetInstance() {
        _singleton ??= new();
        return _singleton;
    }
}