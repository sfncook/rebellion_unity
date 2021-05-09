public class PersonnelType : AbstractType
{
    public readonly int attackStrength;
    public readonly int defenseStrength;
    public readonly bool isStealth;

    public PersonnelType(string name) : base(name)
    {
    }
}
