public class Personnel : AbstractUnit
{
    public readonly Team team;
    public int manyPeople;
    public int manyPeopleDead; // to be removed between events

    public Personnel(PersonnelType personnelType, Team team) : base(personnelType)
    {
        this.team = team;
        this.manyPeople = personnelType.totalManyPeople;
        this.manyPeopleDead = 0;
    }
}
