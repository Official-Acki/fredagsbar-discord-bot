using MySqlConnector;

interface DatabaseObj<T> {
    abstract void CreateObj();
    abstract static List<T> ReadToObjs(MySqlDataReader mySqlDataReader);
    abstract void UpdateObj();
    abstract void DeleteObj();
}