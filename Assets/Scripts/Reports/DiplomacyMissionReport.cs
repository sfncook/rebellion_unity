public class DiplomacyMissionreport: MissionReport
{ 
    public float loyaltyDelta;

    public DiplomacyMissionreport(
        Personnel reporter,
        MissionType missionType,
        bool success,
        float loyaltyDelta
    ) : base(reporter,missionType, success)
    {
        this.loyaltyDelta = loyaltyDelta;
    }
}
