public class DefenseType : AbstractType
{
    public static DefenseType orbitalBattery = new DefenseType("Orbital Battery", attackStrength:4);
    public static DefenseType planetaryShield = new DefenseType("Planetary Shield", defenseModifer:6);

    public readonly int attackStrength;
    public readonly int defenseModifer;
    public readonly int fullHealth = 10;

    public DefenseType(string name, int attackStrength = 0, int defenseModifer = 0) : base(name)
    {
        this.attackStrength = attackStrength;
        this.defenseModifer = defenseModifer;
    }
}
