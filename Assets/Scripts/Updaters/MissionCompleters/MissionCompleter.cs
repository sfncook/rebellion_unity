public abstract class MissionCompleter
{
    public abstract MissionReport completeMission(StarSector sector, Planet planet, Personnel personnel);

    protected bool didMissionSucceed(Personnel personnel, float successThreshold)
    {
        MissionType missionType = personnel.activeMission;
        return UnityEngine.Random.Range(0.0f, 100f) <= successThreshold;
    }
}
