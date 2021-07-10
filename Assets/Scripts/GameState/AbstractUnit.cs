using System;

public class AbstractUnit: AbstractTarget
{
    public readonly AbstractType type;
    public readonly string uuid;
    public int dayArrival = -1; // For travelling
    public MissionType activeMission = null;
    public int dayMissionComplete = -1;
    public AbstractUnit missionTargetUnit;
    public Planet missionTargetPlanet;

    public AbstractUnit(AbstractType type)
    {
        this.type = type;
        this.uuid = Guid.NewGuid().ToString();
    }

    public bool inTransit()
    {
        return dayArrival > MainGameState.gameState.gameTime;
    }

    public bool hasMission()
    {
        return dayMissionComplete > MainGameState.gameState.gameTime;
    }

    public void assignMission(MissionType missionType, AbstractUnit missionTargetUnit=null, Planet missionTargetPlanet=null)
    {
        activeMission = missionType;
        dayMissionComplete = MainGameState.gameState.gameTime + 2;
        this.missionTargetUnit = missionTargetUnit;
        this.missionTargetPlanet = missionTargetPlanet;
    }

    public void resetMission()
    {
        activeMission = null;
        dayMissionComplete = -1;
        missionTargetUnit = null;
        missionTargetPlanet = null;
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
