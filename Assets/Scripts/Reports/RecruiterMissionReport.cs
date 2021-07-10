public class RecruiterMissionReport: MissionReport
{ 
    public Personnel recruitedPersonnel;

    public RecruiterMissionReport(
        Personnel reporter,
        bool success,
        int dayComplete,
        bool showImmediately = false,
        Personnel recruitedPersonnel = null
    ) : base(reporter,MissionType.recruiting, success, dayComplete, showImmediately)
    {
        this.recruitedPersonnel = recruitedPersonnel;
    }
}
