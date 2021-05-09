using System.Collections.Generic;

public class Ship
{
    public readonly ShipType shipType;
    public List<Personnel> personnelsOnBoard = new List<Personnel>();

    public int health;

    public Ship(ShipType shipType)
    {
        this.shipType = shipType;
        this.health = shipType.fullHealth;
    }
}
