using System.Collections.Generic;

public class Planet
{
    // Immutable attributes:
    public readonly string name;

    // Randomized-then-immutable attributes:
    public readonly int energyCapacity;
    public bool isHq = false;

    // Mutable state:
    public float loyalty; // 0=TeamA & 1=TeamB
    public readonly List<Ship> shipsInOrbit = new List<Ship>();
    public readonly List<Ship> shipsInTransit = new List<Ship>();
    public readonly List<Personnel> personnelsOnSurface = new List<Personnel>();
    public readonly List<Factory> factories = new List<Factory>();

    public Planet(string name, int energyCapacity, float loyalty)
    {
        this.name = name;
        this.energyCapacity = energyCapacity;
        this.loyalty = loyalty;
    }
}
