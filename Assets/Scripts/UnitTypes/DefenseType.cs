public class DefenseType : AbstractType
{
    public static DefenseType orbitalBattery = new DefenseType("Orbital Battery", 4, 6, 10);
    public static DefenseType planetaryShield = new DefenseType("Planetary Shield", 4, 6, 10);

    public readonly int attackStrength;
    public readonly int defenseModifer;
    public readonly int fullHealth;     // out of 10

    public DefenseType(string name, int attackStrength, int defenseModifer, int fullHealth) : base(name)
    {
        this.attackStrength = attackStrength;
        this.defenseModifer = defenseModifer;
        this.fullHealth = fullHealth;
    }
}
