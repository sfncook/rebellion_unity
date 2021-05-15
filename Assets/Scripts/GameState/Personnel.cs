public class Personnel : AbstractUnit
{
    public readonly Team team;

    public Personnel(PersonnelType personnelType, Team team) : base(personnelType)
    {
        this.team = team;
    }
}
