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

    public override int GetHashCode()
    {
        return uuid.GetHashCode();
    }

    public override bool Equals(Object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            AbstractUnit p = (AbstractUnit)obj;
            return p.uuid.Equals(uuid);
        }
    }
}
