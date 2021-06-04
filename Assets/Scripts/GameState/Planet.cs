using System.Collections.Generic;

public class Planet
{
    // Immutable attributes:
    public readonly string name;
    public readonly float sectorX;
    public readonly float sectorY;

    // Randomized-then-immutable attributes:
    public readonly int energyCapacity;
    public bool isHq = false;

    // Mutable state:
    public float loyalty; // 0=TeamA & 1=TeamB
    public bool isInConflict = false;
    public readonly List<Ship> shipsInOrbit = new List<Ship>();
    public readonly List<Ship> shipsInTransit = new List<Ship>();

    public readonly List<Personnel> personnelsOnSurface = new List<Personnel>();
    public readonly List<Personnel> personnelsInTransit = new List<Personnel>();

    public readonly List<Factory> factories = new List<Factory>();
    public readonly List<Factory> factoriesInTransit = new List<Factory>();

    public readonly List<Defense> defenses = new List<Defense>();
    public readonly List<Defense> defensesInTransit = new List<Defense>();

    public Planet(string name, int energyCapacity, float loyalty) : this(name, energyCapacity, loyalty, 0f, 0f)
    {
        
    }

    public Planet(string name, int energyCapacity, float loyalty, float sectorX, float sectorY)
    {
        this.name = name;
        this.energyCapacity = energyCapacity;
        this.loyalty = loyalty;
        this.sectorX = sectorX;
        this.sectorY = sectorY;
    }
}
