public class RecruiterMissionReport: MissionReport
{ 
    public Personnel recruitedPersonnel;

    public RecruiterMissionReport(
        Personnel reporter,
        bool success,
        int dayComplete,
        Personnel recruitedPersonnel = null
    ) : base(reporter,MissionType.recruiting, success, dayComplete)
    {
        this.recruitedPersonnel = recruitedPersonnel;
    }
}
