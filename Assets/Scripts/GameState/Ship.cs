using System.Collections.Generic;

public class Ship : AbstractUnit
{
    public List<Personnel> personnelsOnBoard = new List<Personnel>();

    public float health;
    public readonly Team team;

    public Ship(ShipType shipType, Team team) : base(shipType)
    {
        this.health = shipType.fullHealth;
        this.team = team;
    }
}
