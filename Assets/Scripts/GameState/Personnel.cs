public class Personnel : AbstractUnit
{
    public readonly Team team;
    public int manyPeople;
    public int manyPeopleDead; // to be removed between events

    // Attributes for heros
    public Hero hero;
    public float visibility; // out of 100 where 0=full-stealth, 100=completely-visible

    // Mission qualifications (out of 100)
    public float espionage;   // Likelihood that they will collect new intelligence
    public float recruiting;  // Likelihood that they will recruit someone new
    public float diplomacy;   // Likelihood that they will successfully modify planet sentiment


    public Personnel(
        PersonnelType personnelType,
        Team team,
        Hero hero = null,
        float visibility = -1,
        float espionage = -1,
        float recruiting = -1,
        float diplomacy = -1
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
        return getNextReportForPersonnel() != null;
    }

    public Report getNextReportForPersonnel()
    {
        foreach (Report report in MainGameState.gameState.reportsUnAcked)
        {
            if (report.reporter.Equals(this))
            {
                return report;
            }
        }
        return null;
    }
}
