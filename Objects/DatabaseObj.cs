using Npgsql;

interface DatabaseObj<T> {
    abstract void CreateObj();
    abstract static List<T> ReadToObjs(NpgsqlDataReader mySqlDataReader);
    abstract void UpdateObj();
    abstract void DeleteObj();
}