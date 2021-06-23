public class DiplomacyMissionReport: MissionReport
{ 
    public float loyaltyDelta;

    public DiplomacyMissionReport(
        Personnel reporter,
        bool success,
        float loyaltyDelta=0
    ) : base(reporter,MissionType.diplomacy, success)
    {
        this.loyaltyDelta = loyaltyDelta;
    }
}
