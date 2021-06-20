public class Personnel : AbstractUnit
{
    public readonly Team team;
    public int manyPeople;
    public int manyPeopleDead; // to be removed between events

    // Attributes for heros
    public Hero hero;

    public Personnel(
        PersonnelType personnelType,
        Team team,
        Hero hero=null
    ) : base(personnelType)
    {
        this.team = team;
        this.manyPeople = personnelType.totalManyPeople;
        this.manyPeopleDead = 0;

        this.hero = hero;
    }

    public bool isHero()
    {
        return hero != null;
    }
}
