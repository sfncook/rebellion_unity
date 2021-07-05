public class EspionageMissionReport: MissionReport
{
    public EspionageMissionReport(
        Personnel reporter,
        bool success,
        int dayComplete
    ) : base(reporter,MissionType.recruiting, success, dayComplete)
    {
    }
}
