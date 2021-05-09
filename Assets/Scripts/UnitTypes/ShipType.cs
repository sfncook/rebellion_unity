public class ShipType
{
    public static ShipType Bireme = new ShipType("Bireme", 2, 2, 2, false, 4);
    public static ShipType Trireme = new ShipType("Trireme", 3, 3, 3, false, 6);
    public static ShipType Quadreme = new ShipType("Quadreme", 4, 4, 4, false, 8);
    public static ShipType Quintreme = new ShipType("Quintreme", 2, 2, 3, true, 4);

    public readonly string name;
    public readonly int personnelCapacity;
    public readonly int attackStrength;     // out of 10
    public readonly int defenseStrength;    // out of 10
    public readonly bool isStealth;
    public readonly int fullHealth;         // out of 10

    public ShipType(
        string name,
        int personnelCapacity,
        int attackStrength,
        int defenseStrength,
        bool isStealth,
        int fullHealth)
    {
        this.name = name;
        this.personnelCapacity = personnelCapacity;
        this.attackStrength = attackStrength;
        this.defenseStrength = defenseStrength;
        this.isStealth = isStealth;
        this.fullHealth = fullHealth;
    }
}
