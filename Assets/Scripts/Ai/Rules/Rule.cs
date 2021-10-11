using System.Collections.Generic;

public abstract class Rule
{
    public abstract void apply(Dictionary<AbstractUnit, UnitAction> unitActions);
}
