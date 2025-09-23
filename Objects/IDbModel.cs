public interface IDbModel<T>
{
    static abstract T ReadObj(int id);
    static abstract IEnumerable<T> GetAll();
}