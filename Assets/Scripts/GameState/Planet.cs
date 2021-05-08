using System.Collections.Generic;

[System.Serializable]
public class Planet
{
    // Immutable attributes:
    public string name;

    // Randomized-then-immutable attributes:
    public int energyCapacity;

    // Mutable state:
    public float loyalty;
    public List<SpaceShip> shipsInOrbit;
    public List<SpaceShip> shipsInTransit;
    public List<Personnel> personnelsOnSurface;

    public Planet(string name, int energyCapacity, float loyalty)
    {
        this.name = name;
        this.energyCapacity = energyCapacity;
        this.loyalty = loyalty;
    }
}
