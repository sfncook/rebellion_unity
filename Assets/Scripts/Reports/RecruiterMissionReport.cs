public class RecruiterMissionReport: MissionReport
{ 
    public Personnel recruitedPersonnel;

    public RecruiterMissionReport(
        Personnel reporter,
        bool success,
        Personnel recruitedPersonnel = null
    ) : base(reporter,MissionType.recruiting, success)
    {
        this.recruitedPersonnel = recruitedPersonnel;
    }
}
