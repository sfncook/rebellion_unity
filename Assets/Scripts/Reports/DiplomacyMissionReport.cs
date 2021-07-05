public class DiplomacyMissionReport: MissionReport
{ 
    public float loyaltyDelta;

    public DiplomacyMissionReport(
        Personnel reporter,
        bool success,
        int dayComplete,
        float loyaltyDelta=0
    ) : base(reporter,MissionType.diplomacy, success, dayComplete)
    {
        this.loyaltyDelta = loyaltyDelta;
    }
}
