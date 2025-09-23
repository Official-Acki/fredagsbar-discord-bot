using Dapper;

namespace det_er_fredag.Objects;

public class CasesOwed: IDbModel<CasesOwed>
{
    public int person_id { get; }
    public float cases { get; }
    public DateTime updated_at { get; }

    public static CasesOwed ReadObj(int person_id)
    {
        return DatabaseController.GetInstance().db.Query<CasesOwed>("SELECT person_id, cases, updated_at from cases_owed WHERE person_id = @person_id", new { person_id = person_id }).First();
    }

    public static IEnumerable<CasesOwed> GetAll()
    {
        return DatabaseController.GetInstance().db.Query<CasesOwed>("SELECT person_id, cases, updated_at from cases_owed").AsList();
    }
}

public class CasesGiven : IDbModel<CasesGiven>
{
    public int person_id { get; }
    public DateTime given_at { get; }
    public float cases { get; }

    public static CasesGiven ReadObj(int id)
    {
        return DatabaseController.GetInstance().db.Query<CasesGiven>("SELECT person_id, given_at, cases from cases_given WHERE person_id = @person_id", new { person_id = id }).First();
    }
    
    public static IEnumerable<CasesGiven> GetAll()
    {
        return DatabaseController.GetInstance().db.Query<CasesGiven>("SELECT person_id, given_at, cases from cases_given").AsList();
    }
}