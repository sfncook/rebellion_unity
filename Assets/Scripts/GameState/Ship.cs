using System.Collections.Generic;

public class Ship
{
    public readonly ShipType shipType;
    public List<Personnel> personnelsOnBoard = new List<Personnel>();

    public int health;
    public readonly Team team;

    public Ship(ShipType shipType, Team team)
    {
        this.shipType = shipType;
        this.health = shipType.fullHealth;
        this.team = team;
    }
}
