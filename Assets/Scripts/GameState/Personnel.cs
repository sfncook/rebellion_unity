public class Personnel : AbstractUnit
{
    public readonly Team team;
    public int manyPeople;
    public int manyPeopleDead; // to be removed between events

    // Attributes for heros
    public Hero hero;
    public int visibility; // out of 100 where 0=full-stealth, 100=completely-visible

    // Mission qualifications (out of 100)
    public int espionage;   // Likelihood that they will collect new intelligence
    public int recruiting;  // Likelihood that they will recruit someone new
    public int diplomacy;   // Likelihood that they will successfully modify planet sentiment


    public Personnel(
        PersonnelType personnelType,
        Team team,
        Hero hero = null,
        int visibility = -1,
        int espionage = -1,
        int recruiting = -1,
        int diplomacy = -1
    ) : base(personnelType)
    {
        this.team = team;
        this.manyPeople = personnelType.totalManyPeople;
        this.manyPeopleDead = 0;

        this.hero = hero;

        this.visibility = (visibility >= 0) ? visibility : personnelType.defaultVisibility;
        this.espionage = (espionage >= 0) ? espionage : personnelType.defaultEspionage;
        this.recruiting = (recruiting >= 0) ? recruiting : personnelType.defaultRecruiting;
        this.diplomacy = (diplomacy >= 0) ? diplomacy : personnelType.defaultDiplomacy;
    }

    public bool isHero()
    {
        return hero != null;
    }

    public bool hasUnAckedReports()
    {
        return MainGameState.gameState.personnelHasUnAckedReports(this);
    }
}
