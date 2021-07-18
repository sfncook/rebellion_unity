public class EspionageMissionReport: MissionReport
{
    public AbstractUnit targetUnit;
    public bool destroyed;

    public EspionageMissionReport(
        Personnel reporter,
        bool success,
        int dayComplete,
        bool showImmediately = false,
        AbstractUnit targetUnit = null,
        bool destroyed = false
    ) : base(reporter,MissionType.recruiting, success, dayComplete, showImmediately)
    {
        this.targetUnit = targetUnit;
        this.destroyed = destroyed;
    }
}
