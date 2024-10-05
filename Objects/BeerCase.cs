namespace det_er_fredag.Objects;

class BeerCase {
    int? dbId;
    DateTime time;
    float amount;
    Person debtor;

    public BeerCase(DateTime time, float amount, Person debtor) {
        this.time = time;
        this.amount = amount;
        this.debtor = debtor;
    }
}