using System.Collections.Generic;

[System.Serializable]
public class Planet
{
    // Immutable attributes:
    public string name;
    public float sectorX;
    public float sectorY;

    // Randomized-then-immutable attributes:
    public int energyCapacity;
    public bool isHq = false;

    // Mutable state:
    public float loyalty; // 0=TeamA & 1=TeamB
    public bool isInConflict = false;
    public List<Ship> shipsInOrbit = new List<Ship>();
    public List<Ship> shipsInTransit = new List<Ship>();

    public List<Personnel> personnelsOnSurface = new List<Personnel>();
    public List<Personnel> personnelsInTransit = new List<Personnel>();

    public List<Factory> factories = new List<Factory>();
    public List<Factory> factoriesInTransit = new List<Factory>();

    public List<Defense> defenses = new List<Defense>();
    public List<Defense> defensesInTransit = new List<Defense>();

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
