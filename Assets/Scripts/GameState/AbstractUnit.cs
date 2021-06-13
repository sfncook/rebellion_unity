using System;

public class AbstractUnit
{
    public readonly AbstractType type;
    public readonly string uuid;
    public AbstractUnit(AbstractType type)
    {
        this.type = type;
        this.uuid = Guid.NewGuid().ToString();
    }
}
