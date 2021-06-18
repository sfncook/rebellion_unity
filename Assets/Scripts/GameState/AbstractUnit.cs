using System;

public class AbstractUnit
{
    public readonly AbstractType type;
    public readonly string uuid;
    public int dayArrival = 0; // For travelling

    public AbstractUnit(AbstractType type)
    {
        this.type = type;
        this.uuid = Guid.NewGuid().ToString();
    }

    public bool inTransit()
    {
        return dayArrival > MainGameState.gameState.gameTime;
    }
}
