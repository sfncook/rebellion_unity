public class Personnel : AbstractUnit
{
    public readonly Team team;
    public int manyPeople;
    public int manyPeopleDead; // to be removed between events

    // Attributes for individuals
    public string moniker; // "human" name

    public Personnel(
        PersonnelType personnelType,
        Team team,
        string moniker = ""
    ) : base(personnelType)
    {
        this.team = team;
        this.manyPeople = personnelType.totalManyPeople;
        this.manyPeopleDead = 0;

        this.moniker = moniker;
    }
}
