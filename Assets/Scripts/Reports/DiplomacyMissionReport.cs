public class DiplomacyMissionReport: MissionReport
{ 
    public float loyaltyDelta;
    public bool loyaltyLost;
    public float loyaltyLostDelta;

    public DiplomacyMissionReport(
        Personnel reporter,
        bool success,
        int dayComplete,
        bool showImmediately = false,
        float loyaltyDelta=0,
        bool loyaltyLost=false,
        float loyaltyLostDelta=0
    ) : base(reporter,MissionType.diplomacy, success, dayComplete, showImmediately)
    {
        this.loyaltyDelta = loyaltyDelta;
        this.loyaltyLost = loyaltyLost;
        this.loyaltyLostDelta = loyaltyLostDelta;
    }
}
