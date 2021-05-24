public class ShipType : AbstractType
{
    public static ShipType Bireme = new ShipType("Bireme", 2, 2, 2, false, 4, 0);
    public static ShipType Trireme = new ShipType("Trireme", 3, 3, 3, false, 6, 1);
    public static ShipType Quadreme = new ShipType("Quadreme", 4, 4, 4, false, 8, 2);
    public static ShipType Quintreme = new ShipType("Quintreme", 2, 2, 3, true, 4, 2);

    public readonly int personnelCapacity;
    public readonly int attackStrength;     // out of 10
    public readonly int defenseStrength;    // out of 10
    public readonly bool isStealth;
    public readonly float fullHealth;         // out of 10
    public readonly int manyAttacksPerTurn; 

    public ShipType(
        string name,
        int personnelCapacity,
        int attackStrength,
        int defenseStrength,
        bool isStealth,
        int fullHealth,
        int manyAttacksPerTurn) : base(name)
    {
        this.personnelCapacity = personnelCapacity;
        this.attackStrength = attackStrength;
        this.defenseStrength = defenseStrength;
        this.isStealth = isStealth;
        this.fullHealth = fullHealth;
        this.manyAttacksPerTurn = manyAttacksPerTurn;
    }
}
