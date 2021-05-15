public class PersonnelType : AbstractType
{
    public static PersonnelType Soldiers = new PersonnelType("Soldiers", false);
    public static PersonnelType Diplomat = new PersonnelType("Diplomat", false);
    public static PersonnelType Spy = new PersonnelType("Spy", true);

    public readonly bool isStealth;

    public PersonnelType(
        string name,
        bool isStealth
        ) : base(name)
    {
        this.isStealth = isStealth;
    }
}
