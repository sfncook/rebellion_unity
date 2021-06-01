public class PersonnelType : AbstractType
{
    public static PersonnelType Soldiers = new PersonnelType("Soldiers", false, totalManyPeople: 10, canAttack:true, attackAccuracyPerc: 50, defenseModifier: 10);
    public static PersonnelType Diplomat = new PersonnelType("Diplomat", false, totalManyPeople: 1, canAttack: false);
    public static PersonnelType Spy = new PersonnelType("Spy", true, totalManyPeople: 1, canAttack: false);

    public readonly bool isStealth;
    public readonly bool isMultiple;
    public readonly int totalManyPeople;
    public readonly bool canAttack;
    public readonly int attackAccuracyPerc; // Out of 100
    public readonly int defenseModifier; // Out of 100

    public PersonnelType(
        string name,
        bool isStealth,
        int totalManyPeople,
        bool canAttack,
        int attackAccuracyPerc = 0,
        int defenseModifier = 0
        ) : base(name, TypeCategory.Personnel)
    {
        this.isStealth = isStealth;
        this.totalManyPeople = totalManyPeople;
        this.canAttack = canAttack;
        this.attackAccuracyPerc = attackAccuracyPerc;
        this.defenseModifier = defenseModifier;
    }
}
