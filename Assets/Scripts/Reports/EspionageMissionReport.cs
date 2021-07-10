public class EspionageMissionReport: MissionReport
{
    public EspionageMissionReport(
        Personnel reporter,
        bool success,
        int dayComplete,
        bool showImmediately = false
    ) : base(reporter,MissionType.recruiting, success, dayComplete, showImmediately)
    {
    }
}
