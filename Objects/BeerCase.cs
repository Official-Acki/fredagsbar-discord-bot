using Npgsql;

namespace det_er_fredag.Objects;

class BeerCase : DatabaseObj<BeerCase>
{
    int? dbId;
    // Currently owed
    float amount;
    // Given history
    private class GivenBeerCase
    {
        public int PersonId { get; set; }
        public DateTime GivenAt { get; set; }
        public float Amount { get; set; }

        public GivenBeerCase(int personId, DateTime givenAt, float amount)
        {
            PersonId = personId;
            GivenAt = givenAt;
            Amount = amount;
        }
    }
    List<GivenBeerCase> givenCases = new();
    int? personId;
    DateTime time;

    public BeerCase(DateTime time, float amount, Person debtor)
    {
        this.time = time;
        this.amount = amount;
        this.personId = debtor.dbId;
    }

    public void CreateObj()
    {
        throw new NotImplementedException();
    }

    public static List<BeerCase> ReadToObjs(NpgsqlDataReader mySqlDataReader)
    {
        throw new NotImplementedException();
    }

    public void DeleteObj()
    {
        throw new NotImplementedException();
    }

    public void UpdateObj()
    {
        throw new NotImplementedException();
    }
}