using System.Collections.Generic;
using System.Linq;

public abstract class Rule
{
    public abstract void apply(Dictionary<AbstractUnit, UnitAction> unitActions);

    protected int personnelOnSurfaceForTeam(Planet planet, Team team)
    {
        return planet.personnelsOnSurface.Where(p => p.team == team).Sum(p => p.manyPeople);
    }
}
