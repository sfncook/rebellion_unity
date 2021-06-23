public class DiplomacyMissionreport: MissionReport
{ 
    public float loyaltyDelta;

    public DiplomacyMissionreport(
        Personnel reporter,
        bool success,
        float loyaltyDelta=0
    ) : base(reporter,MissionType.diplomacy, success)
    {
        this.loyaltyDelta = loyaltyDelta;
    }
}
