using System.Collections.Generic;

[System.Serializable]
public class Planet
{
    // Immutable attributes:
    public string name;

    // Randomized-then-immutable attributes:
    public int energyCapacity;

    // Mutable state:
    public bool discovered = true;
    public float loyalty;
    public List<SpaceShip> shipsInOrbit;
    public List<SpaceShip> shipsInTransit;
    public List<Personnel> personnelsOnSurface;
}
